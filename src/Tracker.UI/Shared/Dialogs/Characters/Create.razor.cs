using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.UI.Library.Features.State;
using Tracker.UI.Library.StateProviders;

namespace Tracker.UI.Shared.Dialogs.Characters;

public partial class Create
{
    private readonly CreateCharacterRequest _request = new();

    [Parameter] public string ButtonText { get; set; } = null!;

    [Inject] private IState<CharacterState> CharacterState { get; set; } = null!;

    [Inject] private ICharacterStateProvider CharacterStateProvider { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        CharacterStateProvider.CreateCharacter(_request);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
