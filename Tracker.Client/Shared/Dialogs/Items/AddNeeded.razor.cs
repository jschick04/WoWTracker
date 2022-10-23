using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Helpers;

namespace Tracker.Client.Shared.Dialogs.Items;

public partial class AddNeeded
{
    private readonly NeededItemRequest _request = new();

    private ElementReference _cancelButton;
    private bool _isLoading;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await _cancelButton.FocusAsync();
        }
    }

    private void Cancel() => Modal.CancelAsync();

    private List<string>? GetAvailableItems()
    {
        if (string.IsNullOrWhiteSpace(_request.Profession)) { return null; }

        var items = ItemManager.Items?[_request.Profession];

        if (items?.Count > 0)
        {
            return items.Select(item => item.Name).ToList();
        }

        _request.Name = string.Empty;
        return null;
    }

    private async Task Submit()
    {
        _isLoading = true;

        //var response = await CharacterManager.AddNeededItemAsync(Id, _request);

        //if (response.Succeeded)
        //{
        //    ToastService.ShowSuccess($"{_request.Name} has been added");
        //}
        //else
        //{
        //    response.ToastError(ToastService);
        //}

        _isLoading = false;

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
