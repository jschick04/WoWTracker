using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using FluentResults;
using Microsoft.AspNetCore.Components.Authorization;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Shared.Constants.Storage;

namespace Tracker.UI.Library.Managers.Authentication;

public class AuthenticationManager : IAuthenticationManager
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly SemaphoreSlim _refreshLock = new(1, 1);
    private volatile bool _isRefreshing;

    public AuthenticationManager(
        AuthenticationStateProvider authenticationStateProvider,
        HttpClient httpClient,
        ILocalStorageService localStorage
    )
    {
        _authenticationStateProvider = authenticationStateProvider;
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<ClaimsPrincipal> GetCurrentUserClaims()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return state.User;
    }

    public async Task<Result> Login(AuthenticationRequest request)
    {
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.PostAsJsonAsync(ApiRoutes.Identity.AuthenticateUri, request);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (result is null)
        {
            return Result.Fail("Failed to get Authentication Response");
        }

        await _localStorage.SetItemAsStringAsync(StorageConstants.AuthToken, result.Token);

        if (response.Headers.TryGetValues("RefreshToken", out var refresh))
        {
            await _localStorage.SetItemAsStringAsync(StorageConstants.RefreshToken, refresh.FirstOrDefault());
        }

        ((ClientAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.Id);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

        return Result.Ok();
    }

    public async Task<Result> Logout()
    {
        // TODO: Should we revoke the token first?
        await _localStorage.RemoveItemAsync(StorageConstants.AuthToken);
        await _localStorage.RemoveItemAsync(StorageConstants.RefreshToken);

        ((ClientAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;

        return Result.Ok();
    }

    public async Task<string?> TryRefreshToken()
    {
        var remainingMinutes = await GetTokenExpirationTime();

        if (remainingMinutes > 2 || (_isRefreshing && remainingMinutes > 0))
        {
            return await _localStorage.GetItemAsStringAsync(StorageConstants.AuthToken);
        }

        return await ForceRefreshToken();
    }

    private async Task<string?> ForceRefreshToken()
    {
        await _refreshLock.WaitAsync();

        var remainingMinutes = await GetTokenExpirationTime();

        if (remainingMinutes > 2)
        {
            return await _localStorage.GetItemAsStringAsync(StorageConstants.AuthToken);
        }

        try
        {
            _isRefreshing = true;
            return await RefreshToken();
        }
        finally
        {
            _isRefreshing = false;
            _refreshLock.Release();
        }
    }

    private async Task<double> GetTokenExpirationTime()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var exp = authState.User.FindFirst(c => c.Type.Equals("exp"))?.Value;
        var expires = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));

        return (expires - DateTime.UtcNow).TotalMinutes;
    }

    private async Task<string?> RefreshToken()
    {
        var response = await _httpClient.PostAsync(ApiRoutes.Identity.RefreshTokenUri, null);

        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException($"Refresh Token Failed: Error: {response.StatusCode}");
        }

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

        if (result is null) { return null; }

        await _localStorage.SetItemAsStringAsync(StorageConstants.AuthToken, result.Token);

        if (response.Headers.TryGetValues("RefreshToken", out var refresh))
        {
            await _localStorage.SetItemAsStringAsync(StorageConstants.RefreshToken, refresh.FirstOrDefault());
        }

        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

        return result.Token;
    }
}
