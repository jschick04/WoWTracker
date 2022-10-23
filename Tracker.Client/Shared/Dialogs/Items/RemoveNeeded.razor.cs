using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Helpers;

namespace Tracker.Client.Shared.Dialogs.Items;

public partial class RemoveNeeded
{
    private bool _isLoading;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [Parameter] public NeededItemRequest Item { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        _isLoading = true;
        //var response = await CharacterManager.RemoveNeededItemAsync(Id, Item);

        //if (response.Succeeded)
        //{
        //    ToastService.ShowSuccess($"{Item.Name} has been removed");
        //}
        //else
        //{
        //    response.ToastError(ToastService);
        //}

        _isLoading = false;
        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
