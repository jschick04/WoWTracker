using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Toolbelt.Blazor;
using Tracker.Api.Contracts.Routes;
using Tracker.Client.Core.Managers.Authentication;

namespace Tracker.Client.Core.Managers.Interceptors {

    public class HttpInterceptorManager : IHttpInterceptorManager {

        private readonly IAuthenticationManager _authenticationManager;
        private readonly HttpClientInterceptor _interceptor;
        private readonly ILogger<HttpInterceptorManager> _logger;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public HttpInterceptorManager(
            IAuthenticationManager authenticationManager,
            HttpClientInterceptor interceptor,
            ILogger<HttpInterceptorManager> logger,
            NavigationManager navigationManager,
            ISnackbar snackbar
        ) {
            _authenticationManager = authenticationManager;
            _interceptor = interceptor;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
            _logger = logger;
        }

        public void DisposeEvent() {
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
        }

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e) {
            if (ValidatePath(e.Request.RequestUri?.AbsolutePath)) {
                try {
                    var token = await _authenticationManager.TryRefreshToken();

                    if (!string.IsNullOrEmpty(token)) {
                        _logger.LogDebug("InterceptBeforeHttpAsync: Successfully Refreshed Token");
                        e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                } catch (Exception exception) {
                    _logger.LogError("InterceptBeforeHttpAsync Failure: {0}", exception.Message);
                    _snackbar.Add("You are not Logged In", Severity.Error);
                    await _authenticationManager.Logout();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        public void RegisterEvent() {
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        }

        private static bool ValidatePath(string uri) {
            var ignorePaths = new[] {
                ApiRoutes.Identity.PathUri,
                ApiRoutes.Account.ForgotPassword,
                ApiRoutes.Account.ResetPassword,
                ApiRoutes.Account.VerifyEmail
            };

            if (uri is null) { return false; }

            foreach (var path in ignorePaths) {
                if (uri.Contains(path)) { return false; }
            }

            return true;
        }

    }

}