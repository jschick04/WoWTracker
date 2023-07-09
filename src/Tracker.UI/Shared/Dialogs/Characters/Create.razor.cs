using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.UI.Shared.Dialogs.Characters;

public partial class Create
{
    private readonly CreateCharacterRequest _request = new();

    [Parameter] public string ButtonText { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        CharacterStateProvider.CreateCharacter(_request);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
