﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Tracker.Library.Constants.Storage;
using Tracker.Library.Helpers;

namespace Tracker.Client.Core.Managers.Authentication {

    public class ApiAuthenticationStateProvider : AuthenticationStateProvider {

        private readonly AuthenticationState _anonymous;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage) {
            _httpClient = httpClient;
            _localStorage = localStorage;

            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async Task<ClaimsPrincipal> GetAuthenticationStateProviderUserAsync() {
            var state = await GetAuthenticationStateAsync();

            return state.User;
        }

        public bool IsAnonymous { get; private set; }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            var savedToken = await _localStorage.GetItemAsync<string>(StorageConstants.AuthToken);

            if (string.IsNullOrWhiteSpace(savedToken)) {
                IsAnonymous = true;
                return _anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);

            var state = new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(savedToken), "jwt"))
            );

            IsAnonymous = false;

            return state;
        }

        public void MarkUserAsAuthenticated(int id) {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, id.ToString()) }, "jwt")
            );

            var authState = Task.FromResult(new AuthenticationState(user));
            IsAnonymous = false;

            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut() {
            var authState = Task.FromResult(_anonymous);
            IsAnonymous = true;

            NotifyAuthenticationStateChanged(authState);
        }

    }

}