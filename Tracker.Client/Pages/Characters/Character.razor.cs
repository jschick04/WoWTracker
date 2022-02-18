using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Shared.Dialogs.Characters;

namespace Tracker.Client.Pages.Characters;

public partial class Character {

    private CharacterResponse _character = null!;
    private bool _isLoading;

    [Parameter] public int Id { get; set; }

    protected override async Task OnParametersSetAsync() {
        await UpdateCharacterAsync();
    }

    private async Task DeleteAsync() {
        var parameters = new DialogParameters {
            { nameof(Delete.ContextText), $"Are you sure you want to delete {_character.Name}?" },
            { nameof(Delete.ButtonText), "Delete" },
            { nameof(Delete.Color), Color.Error },
            { nameof(Delete.Id), _character.Id }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        var dialog = _dialogService.Show<Delete>("Delete", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled) {
            await _appStateProvider.UpdateCharactersAsync();
            _navigationManager.NavigateTo("/");
        }
    }

    private async Task UpdateAsync() {
        var parameters = new DialogParameters {
            { nameof(Update.Character), _character },
            { nameof(Update.ButtonText), "Update" },
            { nameof(Update.Color), Color.Success }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        var dialog = _dialogService.Show<Update>("Update", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled) {
            await _appStateProvider.UpdateCharactersAsync();
            await UpdateCharacterAsync();
        }
    }

    private async Task UpdateCharacterAsync() {
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