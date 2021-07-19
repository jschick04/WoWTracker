using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Responses;

namespace Tracker.Client.Shared {

    public partial class NavMenu : IDisposable {

        private bool _isLoading = true;

        private List<CharacterResponse> Characters { get; set; } = new();

        public void Dispose() => _appStateProvider.OnChangeAsync -= UpdateCharactersAsync;

        protected override async Task OnInitializedAsync() {
            _appStateProvider.OnChangeAsync += UpdateCharactersAsync;

            await _appStateProvider.UpdateCharactersAsync();
        }

        private void LoadCharacter(int id) {
            _navigationManager.NavigateTo($"/character/{id}");
        }

        private async Task UpdateCharactersAsync() {
            _isLoading = true;
            StateHasChanged();

            Characters = await _appStateProvider.GetCharactersAsync();

            _isLoading = false;
            StateHasChanged();
        }

    }

}