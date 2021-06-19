using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Toolbelt.Blazor;
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

        public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;

        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e) {
            var path = e.Request.RequestUri?.AbsolutePath;

            if (path is not null && !path.Contains("identity")) {
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

        public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;

    }

}