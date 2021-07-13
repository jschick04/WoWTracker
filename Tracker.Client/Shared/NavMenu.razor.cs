using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Shared.Dialogs.Characters;
using Tracker.Library.Helpers;

namespace Tracker.Client.Shared {

    public partial class NavMenu : IDisposable {

        private List<CharacterResponse> Characters { get; set; } = new();

        private string Welcome { get; set; } = "";

        public void Dispose() => _appStateProvider.OnChange -= UpdateCharacters;

        protected override async Task OnInitializedAsync() {
            var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();
            
            _appStateProvider.OnChange += UpdateCharacters;

            if (user.Identity?.IsAuthenticated is true) {
                Welcome = $"Welcome {user.GetFirstName()}";
                await _appStateProvider.UpdateCharactersAsync();
            }

        }

        private async Task Delete(int id, string name) {
            var parameters = new DialogParameters {
                { nameof(Dialogs.Characters.Delete.ContextText), $"Are you sure you want to delete {name}?" },
                { nameof(Dialogs.Characters.Delete.ButtonText), "Delete" },
                { nameof(Dialogs.Characters.Delete.Color), Color.Error },
                { nameof(Dialogs.Characters.Delete.Id), id }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = _dialogService.Show<Delete>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled) {
                await _appStateProvider.UpdateCharactersAsync();
            }
        }

        private void UpdateCharacters() {
            Characters = _appStateProvider.Characters;
            StateHasChanged();
        }

    }

}