using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.UI.Library.Features.State;
using Tracker.UI.Library.StateProviders;

namespace Tracker.UI.Shared.Dialogs.Items;

public partial class AddNeeded
{
    private readonly NeededItemRequest _request = new();

    private ElementReference _cancelButton;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string CharacterName { get; set; } = null!;

    [Parameter] public string Id { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    [Inject] private INeededItemStateProvider NeededItemStateProvider { get; set; } = null!;

    [Inject] private IState<ProfessionState> ProfessionState { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _cancelButton.FocusAsync();
        }
    }

    private void Cancel() => Modal.CancelAsync();

    private IEnumerable<string> GetAvailableItems()
    {
        if (string.IsNullOrWhiteSpace(_request.Profession)) { return Enumerable.Empty<string>(); }

        var items = ProfessionState.Value.Items[_request.Profession].ToList();

        if (items.Any())
        {
            return items.Select(item => item.Name);
        }

        _request.Name = string.Empty;

        return Enumerable.Empty<string>();
    }

    private async Task Submit()
    {
        NeededItemStateProvider.AddNeededItem(Id, CharacterName, _request);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
