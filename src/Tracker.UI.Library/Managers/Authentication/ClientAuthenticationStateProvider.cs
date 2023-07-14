using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Tracker.Shared.Constants.Storage;
using Tracker.Shared.Helpers;

namespace Tracker.UI.Library.Managers.Authentication;

public class ClientAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationState _anonymous;
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public ClientAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;

        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public async Task<ClaimsPrincipal> GetAuthenticationStateProviderUserAsync()
    {
        var state = await GetAuthenticationStateAsync();

        return state.User;
    }

    public bool IsAnonymous { get; private set; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorage.GetItemAsStringAsync(StorageConstants.AuthToken);

        if (string.IsNullOrWhiteSpace(savedToken))
        {
            IsAnonymous = true;
            return _anonymous;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

        IsAnonymous = false;

        return new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(savedToken), "jwt"))
        );
    }

    public void MarkUserAsAuthenticated(int id)
    {
        var user = new ClaimsPrincipal(
            new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, id.ToString()) }, "jwt")
        );

        IsAnonymous = false;

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void MarkUserAsLoggedOut()
    {
        IsAnonymous = true;

        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
    }
}
