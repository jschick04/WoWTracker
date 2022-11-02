using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Helpers;
using Tracker.Client.Shared.Dialogs.Characters;
using Tracker.Client.Shared.Dialogs.Items;

namespace Tracker.Client.Pages.Characters;

public partial class Character
{
    [Parameter] public int Id { get; set; }

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    protected override void OnParametersSet() => InitializeState();

    private void AddNeededItemDialog()
    {
        if (CharacterState.Value.Selected?.Name is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(AddNeeded.ButtonText), "Add");
        parameters.Add(nameof(AddNeeded.Id), CharacterState.Value.Selected.Id);
        parameters.Add(nameof(AddNeeded.CharacterName), CharacterState.Value.Selected.Name);

        DialogService.Show<AddNeeded>("Add Needed Item", parameters, options);
    }

    private void DeleteDialog()
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Delete.ContextText),
            $"Are you sure you want to delete {CharacterState.Value.Selected.Name}?");

        parameters.Add(nameof(Delete.ButtonText), "Delete");
        parameters.Add(nameof(Delete.Id), CharacterState.Value.Selected.Id);

        DialogService.Show<Delete>("Delete Character Confirmation", parameters, options);
    }

    private void InitializeState()
    {
        CharacterStateProvider.SetSelectedCharacter(Id);
        NeededItemStateProvider.GetAllNeededItems(Id);
    }

    private void RemoveNeededItemDialog(string profession, string name)
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(RemoveNeeded.ContextText), $"Are you sure you want to remove {name}");
        parameters.Add(nameof(RemoveNeeded.ButtonText), "Remove");
        parameters.Add(nameof(RemoveNeeded.Id), CharacterState.Value.Selected.Id);
        parameters.Add(nameof(RemoveNeeded.Item), new NeededItemRequest { Profession = profession, Name = name });

        DialogService.Show<RemoveNeeded>("Remove Needed Item Confirmation", parameters, options);
    }

    private void UpdateDialog()
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Update.Character), CharacterState.Value.Selected);
        parameters.Add(nameof(Update.ButtonText), "Update");

        DialogService.Show<Update>($"Edit {CharacterState.Value.Selected.Name}", parameters, options);
    }
}
