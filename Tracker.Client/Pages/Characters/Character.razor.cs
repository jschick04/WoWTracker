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
    private CharacterResponse _character = null!;
    private bool _isLoading;
    private List<NeededItemResponse> _itemsToCraft = new();
    private List<NeededItemResponse> _neededItems = new();

    [Parameter] public int Id { get; set; }

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    protected override async Task OnParametersSetAsync() { await UpdateCharacterAsync(); }

    private async Task AddNeededItem()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(AddNeeded.ButtonText), "Add");
        parameters.Add(nameof(AddNeeded.Id), _character.Id);

        var dialog = DialogService.Show<AddNeeded>("Add Needed Item", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await UpdateNeededItemsAsync();
            await UpdateItemsToCraftAsync();
            StateHasChanged();
        }
    }

    private async Task DeleteAsync()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Delete.ContextText), $"Are you sure you want to delete {_character.Name}?");
        parameters.Add(nameof(Delete.ButtonText), "Delete");
        parameters.Add(nameof(Delete.Id), _character.Id);
        parameters.Add(nameof(Delete.Name), _character.Name);

        var dialog = DialogService.Show<Delete>("Delete Character Confirmation", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await AppStateProvider.UpdateCharactersAsync();
            NavigationManager.NavigateTo("/");
        }
    }

    private async Task RemoveNeededItem(string profession, string name)
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(RemoveNeeded.ContextText), $"Are you sure you want to remove {name}");
        parameters.Add(nameof(RemoveNeeded.ButtonText), "Remove");
        parameters.Add(nameof(RemoveNeeded.Id), _character.Id);
        parameters.Add(nameof(RemoveNeeded.Item), new NeededItemRequest { Profession = profession, Name = name });

        var dialog = DialogService.Show<RemoveNeeded>("Remove Needed Item Confirmation", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await UpdateNeededItemsAsync();
            await UpdateItemsToCraftAsync();
            StateHasChanged();
        }
    }

    private async Task UpdateAsync()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode);

        parameters.Add(nameof(Update.Character), _character);
        parameters.Add(nameof(Update.ButtonText), "Update");

        var dialog = DialogService.Show<Update>($"Edit {_character.Name}", parameters, options);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            await AppStateProvider.UpdateCharactersAsync();
            await UpdateCharacterAsync();
        }
    }

    private async Task UpdateCharacterAsync()
    {
        _isLoading = true;
        StateHasChanged();

        var result = await CharacterManager.GetByIdAsync(Id);

        if (result.GetDataIfSuccess(ref _character))
        {
            await UpdateNeededItemsAsync();
            await UpdateItemsToCraftAsync();

            _isLoading = false;
            StateHasChanged();
        }
        else
        {
            result.ToastError(ToastService);

            NavigationManager.NavigateTo("/");
        }
    }

    private async Task UpdateItemsToCraftAsync()
    {
        List<NeededItemResponse> firstList = new();
        List<NeededItemResponse> secondList = new();

        if (!string.IsNullOrWhiteSpace(_character.FirstProfession))
        {
            var first = await ItemManager.GetCraftableByProfession(_character.FirstProfession);
            first.GetDataIfSuccess(ref firstList);
        }

        if (!string.IsNullOrWhiteSpace(_character.SecondProfession))
        {
            var second = await ItemManager.GetCraftableByProfession(_character.SecondProfession);
            second.GetDataIfSuccess(ref secondList);
        }

        _itemsToCraft = firstList;
        _itemsToCraft.AddRange(secondList);
    }

    private async Task UpdateNeededItemsAsync() =>
        (await CharacterManager.GetNeededItemsAsync(Id)).GetDataIfSuccess(ref _neededItems);
}
