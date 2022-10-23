using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Helpers;
using Tracker.Client.Shared.Dialogs.Characters;
using Tracker.Client.Shared.Dialogs.Items;
using Tracker.Library.Helpers;

namespace Tracker.Client.Pages.Characters;

public partial class Character
{
    private List<NeededItemResponse> _itemsToCraft = new();
    private List<NeededItemResponse> _neededItems = new();

    [Parameter] public int Id { get; set; }

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    protected override void OnParametersSet() => CharacterStateProvider.SetSelectedCharacter(Id);

    private async Task AddNeededItem()
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(AddNeeded.ButtonText), "Add");
        parameters.Add(nameof(AddNeeded.Id), CharacterState.Value.Selected.Id);

        var dialog = DialogService.Show<AddNeeded>("Add Needed Item", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            //await UpdateNeededItemsAsync();
            await UpdateItemsToCraftAsync();
            StateHasChanged();
        }
    }

    private async Task DeleteAsync()
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Delete.ContextText),
            $"Are you sure you want to delete {CharacterState.Value.Selected.Name}?");

        parameters.Add(nameof(Delete.ButtonText), "Delete");
        parameters.Add(nameof(Delete.Id), CharacterState.Value.Selected.Id);
        parameters.Add(nameof(Delete.Name), CharacterState.Value.Selected.Name!);

        var dialog = DialogService.Show<Delete>("Delete Character Confirmation", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            //await AppStateProvider.UpdateCharactersAsync();
            //NavigationManager.NavigateTo("/");
        }
    }

    private async Task RemoveNeededItem(string profession, string name)
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(RemoveNeeded.ContextText), $"Are you sure you want to remove {name}");
        parameters.Add(nameof(RemoveNeeded.ButtonText), "Remove");
        parameters.Add(nameof(RemoveNeeded.Id), CharacterState.Value.Selected.Id);
        parameters.Add(nameof(RemoveNeeded.Item), new NeededItemRequest { Profession = profession, Name = name });

        var dialog = DialogService.Show<RemoveNeeded>("Remove Needed Item Confirmation", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            //await UpdateNeededItemsAsync();
            await UpdateItemsToCraftAsync();
            StateHasChanged();
        }
    }

    private async Task UpdateAsync()
    {
        if (CharacterState.Value.Selected is null) { return; }

        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Update.Character), CharacterState.Value.Selected);
        parameters.Add(nameof(Update.ButtonText), "Update");

        var dialog = DialogService.Show<Update>($"Edit {CharacterState.Value.Selected.Name}", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            //await AppStateProvider.UpdateCharactersAsync();
        }
    }

    private async Task UpdateItemsToCraftAsync()
    {
        List<NeededItemResponse> firstList = new();
        List<NeededItemResponse> secondList = new();

        if (!string.IsNullOrWhiteSpace(CharacterState.Value.Selected?.FirstProfession))
        {
            var first = await ItemManager.GetCraftableByProfession(CharacterState.Value.Selected.FirstProfession);
            first.GetDataIfSuccess(ref firstList);
        }

        if (!string.IsNullOrWhiteSpace(CharacterState.Value.Selected?.SecondProfession))
        {
            var second = await ItemManager.GetCraftableByProfession(CharacterState.Value.Selected.SecondProfession);
            second.GetDataIfSuccess(ref secondList);
        }

        _itemsToCraft = firstList;
        _itemsToCraft.AddRange(secondList);
    }

    //private async Task UpdateNeededItemsAsync() =>
    //    (await CharacterManager.GetNeededItemsAsync(Id)).GetDataIfSuccess(ref _neededItems);
}
