using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.UI.Library.Features.State;
using Tracker.UI.Library.StateProviders;

namespace Tracker.UI.Shared.Dialogs.Items;

public partial class RemoveNeeded
{
    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [Parameter] public NeededItemRequest Item { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    [Inject] private IState<NeededItemState> NeededItemState { get; set; } = null!;

    [Inject] private INeededItemStateProvider NeededItemStateProvider { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        NeededItemStateProvider.RemoveNeededItem(Id, Item);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
