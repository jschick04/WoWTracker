using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor;
using Tracker.Api.Contracts.Routes;
using Tracker.UI.Library.Managers.Authentication;

namespace Tracker.UI.Library.Managers.Interceptors;

public class HttpInterceptorManager : IHttpInterceptorManager
{
    private readonly IAuthenticationManager _authenticationManager;
    private readonly HttpClientInterceptor _interceptor;
    private readonly NavigationManager _navigationManager;

    public HttpInterceptorManager(
        IAuthenticationManager authenticationManager,
        HttpClientInterceptor interceptor,
        NavigationManager navigationManager
    )
    {
        _authenticationManager = authenticationManager;
        _interceptor = interceptor;
        _navigationManager = navigationManager;
    }

    public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;

    public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
    {
        if (ValidatePath(e.Request.RequestUri?.AbsolutePath))
        {
            try
            {
                var token = await _authenticationManager.TryRefreshToken();

                if (!string.IsNullOrEmpty(token))
                {
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch
            {
                await _authenticationManager.Logout();
                _navigationManager.NavigateTo("/");
            }
        }
    }

    public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

    private static bool ValidatePath(string? uri)
    {
        var ignorePaths = new[]
        {
            ApiRoutes.Identity.Uri,
            ApiRoutes.Account.ForgotPasswordUri,
            ApiRoutes.Account.ResetPasswordUri,
            ApiRoutes.Account.VerifyEmailUri
        };

        if (uri is null) { return false; }

        foreach (var path in ignorePaths)
        {
            if (uri.Contains(path)) { return false; }
        }

        return true;
    }
}
