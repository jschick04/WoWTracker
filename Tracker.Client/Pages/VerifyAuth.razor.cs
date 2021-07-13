using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Client.Pages {

    public partial class VerifyAuth : IDisposable {

        private string CurrentUsername { get; set; } = "";

        private List<CharacterResponse> Characters1 { get; set; } = new();

        private List<CharacterResponse> Characters2 { get; set; } = new();

        public void Dispose() => _appStateProvider.OnChange -= UpdateCharacters;

        protected override async Task OnInitializedAsync() {
            var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();

            _appStateProvider.OnChange += UpdateCharacters;

            if (user.Identity?.IsAuthenticated is true) {
                CurrentUsername = user.GetUsername();
                await _appStateProvider.UpdateCharactersAsync();
            }
        }

        private void UpdateCharacters() {
            Characters1 = _appStateProvider.Characters;
            Characters2 = _appStateProvider.Characters;
            StateHasChanged();
        }

    }

}