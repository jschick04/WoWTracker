using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Helpers;
using Tracker.Client.Shared.Dialogs.Characters;
using Tracker.Library.Helpers;

namespace Tracker.Client.Pages.Characters;

public partial class Character {

    private CharacterResponse _character = null!;
    private bool _isLoading;

    [Parameter] public int Id { get; set; }

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    protected override async Task OnParametersSetAsync() {
        await UpdateCharacterAsync();
    }

    private async Task DeleteAsync() {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Delete.ContextText), $"Are you sure you want to delete {_character.Name}?");
        parameters.Add(nameof(Delete.ButtonText), "Delete");
        parameters.Add(nameof(Delete.Id), _character.Id);
        parameters.Add(nameof(Delete.Name), _character.Name);

        var dialog = DialogService.Show<Delete>("Delete Character Confirmation", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled) {
            await AppStateProvider.UpdateCharactersAsync();
            NavigationManager.NavigateTo("/");
        }
    }

    private async Task UpdateAsync() {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Update.Character), _character);
        parameters.Add(nameof(Update.ButtonText), "Update");

        var dialog = DialogService.Show<Update>($"Edit {_character.Name}", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled) {
            await AppStateProvider.UpdateCharactersAsync();
            await UpdateCharacterAsync();
        }
    }

    private async Task UpdateCharacterAsync() {
        _isLoading = true;
        StateHasChanged();

        var result = await CharacterManager.GetByIdAsync(Id);

        if (result.GetDataIfSuccess(ref _character)) {
            _isLoading = false;
            StateHasChanged();
        } else {
            result.ToastError(ToastService);

            NavigationManager.NavigateTo("/");
        }
    }

}