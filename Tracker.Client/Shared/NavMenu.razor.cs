using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Shared.Dialogs.Characters;

namespace Tracker.Client.Shared {

    public partial class NavMenu : IDisposable {

        private bool _isLoading = true;

        private List<CharacterResponse> Characters { get; set; } = new();

        public void Dispose() => _appStateProvider.OnChangeAsync -= UpdateCharactersAsync;

        protected override async Task OnInitializedAsync() {
            _appStateProvider.OnChangeAsync += UpdateCharactersAsync;

            await _appStateProvider.UpdateCharactersAsync();
        }

        private async Task CreateCharacter() {
            var parameters = new DialogParameters {
                { nameof(Create.ButtonText), "Create" }, { nameof(Create.Color), Color.Tertiary }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = _dialogService.Show<Create>("Create", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled) {
                await _appStateProvider.UpdateCharactersAsync();
                _navigationManager.NavigateTo("/");
            }
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