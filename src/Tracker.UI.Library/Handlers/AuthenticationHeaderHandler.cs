﻿using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Tracker.Api.Contracts.Routes;
using Tracker.Shared.Constants.Storage;

namespace Tracker.UI.Library.Handlers;

public class AuthenticationHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AuthenticationHeaderHandler(ILocalStorageService localStorage) => _localStorage = localStorage;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var path = request.RequestUri?.AbsolutePath;

        if (path?.Contains(ApiRoutes.Identity.RefreshTokenUri) is true)
        {
            await AddRefreshTokenHeader(request);
        }

        if (request.Headers.Authorization?.Scheme == "Bearer")
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var token = await _localStorage.GetItemAsStringAsync(StorageConstants.AuthToken, cancellationToken);

        if (string.IsNullOrWhiteSpace(token))
        {
            return await base.SendAsync(request, cancellationToken);
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task AddRefreshTokenHeader(HttpRequestMessage request)
    {
        var refresh = await _localStorage.GetItemAsStringAsync(StorageConstants.RefreshToken);

        if (string.IsNullOrWhiteSpace(refresh))
        { return; }

        if (request.Headers.TryGetValues("RefreshToken", out var _))
        {
            request.Headers.Remove("RefreshToken");
        }

        request.Headers.Add("RefreshToken", refresh);
    }
}
