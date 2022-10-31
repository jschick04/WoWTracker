using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Shared.Dialogs.Items;

public partial class RemoveNeeded
{
    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [Parameter] public NeededItemRequest Item { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        NeededItemStateProvider.RemoveNeededItem(Id, Item);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
