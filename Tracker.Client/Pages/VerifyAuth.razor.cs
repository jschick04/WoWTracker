using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Client.Pages {

    public partial class VerifyAuth : IDisposable {

        private bool _isLoading;

        private string CurrentUsername { get; set; } = "";

        private List<CharacterResponse> Characters1 { get; set; } = new();

        private List<CharacterResponse> Characters2 { get; set; } = new();

        public void Dispose() => _appStateProvider.OnChangeAsync -= UpdateCharactersAsync;

        protected override async Task OnInitializedAsync() {
            var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();

            _appStateProvider.OnChangeAsync += UpdateCharactersAsync;

            if (user.Identity?.IsAuthenticated is true) {
                CurrentUsername = user.GetUsername();

                _isLoading = true;
                StateHasChanged();

                await _appStateProvider.UpdateCharactersAsync();
            }
        }

        private async Task UpdateCharactersAsync() {
            _isLoading = true;
            StateHasChanged();

            Characters1 = await _appStateProvider.GetCharactersAsync();
            Characters2 = await _appStateProvider.GetCharactersAsync();

            _isLoading = false;
            StateHasChanged();
        }

    }

}