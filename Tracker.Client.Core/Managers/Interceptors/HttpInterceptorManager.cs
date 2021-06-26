using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Toolbelt.Blazor;
using Tracker.Api.Contracts.Routes;
using Tracker.Client.Core.Managers.Authentication;

namespace Tracker.Client.Core.Managers.Interceptors {

    public class HttpInterceptorManager : IHttpInterceptorManager {

        private readonly IAuthenticationManager _authenticationManager;
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public HttpInterceptorManager(
            IAuthenticationManager authenticationManager,
            HttpClientInterceptor interceptor,
            NavigationManager navigationManager,
            ISnackbar snackbar
        ) {
            _authenticationManager = authenticationManager;
            _interceptor = interceptor;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }

        public void DisposeEvent() {
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
            _interceptor.AfterSendAsync -= InterceptAfterHttpAsync;
        }

        public async Task InterceptAfterHttpAsync(object sender, HttpClientInterceptorEventArgs e) {
            if (e.Response.StatusCode == HttpStatusCode.Unauthorized) {
                _snackbar.Add("You are not Logged In", Severity.Error);
                await _authenticationManager.Logout();
                _navigationManager.NavigateTo("/");
            }
        }

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e) {
            if (ValidatePath(e.Request.RequestUri?.AbsolutePath)) {
                try {
                    var token = await _authenticationManager.TryRefreshToken();

                    if (!string.IsNullOrEmpty(token)) {
                        //_snackbar.Add("Refreshed Token", Severity.Success);
                        e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                } catch (Exception exception) {
                    Console.WriteLine($"BeforeSendAsync Failure: {exception.Message}");
                    _snackbar.Add("You are not Logged In", Severity.Error);
                    await _authenticationManager.Logout();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        public void RegisterEvent() {
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
            _interceptor.AfterSendAsync += InterceptAfterHttpAsync;
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