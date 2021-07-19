using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Shared.Dialogs.Characters;

namespace Tracker.Client.Pages.Characters {

    public partial class Character {

        private readonly UpdateCharacterRequest _request = new();

        private CharacterResponse _character;
        private bool _isEditing;
        private bool _isLoading;

        [Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync() {
            await UpdateCharacterAsync();
        }

        private async Task Delete(int id, string name) {
            var parameters = new DialogParameters {
                { nameof(Shared.Dialogs.Characters.Delete.ContextText), $"Are you sure you want to delete {name}?" },
                { nameof(Shared.Dialogs.Characters.Delete.ButtonText), "Delete" },
                { nameof(Shared.Dialogs.Characters.Delete.Color), Color.Error },
                { nameof(Shared.Dialogs.Characters.Delete.Id), id }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            var dialog = _dialogService.Show<Delete>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled) {
                await _appStateProvider.UpdateCharactersAsync();
                _navigationManager.NavigateTo("/");
            }
        }

        private void ToggleEditing() => _isEditing = !_isEditing;

        private async Task UpdateAsync() {
            var result = await _characterManager.UpdateAsync(_character.Id, _request);

            if (result.Succeeded) {
                await _appStateProvider.UpdateCharactersAsync();
                await UpdateCharacterAsync();
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

        private async Task UpdateCharacterAsync() {
            _isEditing = false;
            _isLoading = true;
            StateHasChanged();

            var result = await _characterManager.GetByIdAsync(Id);

            if (result.Succeeded) {
                _character = result.Data;
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }

                _navigationManager.NavigateTo("/");
            }

            _isLoading = false;
            StateHasChanged();
        }

    }

}