using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Helpers;

namespace Tracker.Client.Shared.Dialogs.Characters;

public partial class Create
{
    private readonly CreateCharacterRequest _request = new();

    private bool _isLoading;

    [Parameter] public string ButtonText { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        _isLoading = true;

        var response = await CharacterManager.CreateAsync(_request);

        if (response.Succeeded)
        {
            await AppStateProvider.UpdateCharactersAsync();

            ToastService.ShowSuccess($"{_request.Name} has been created");
        }
        else
        {
            response.ToastError(ToastService);
        }

        _isLoading = false;

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
