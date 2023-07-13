using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Library.Features.State;
using Tracker.UI.Library.StateProviders;

namespace Tracker.UI.Shared.Dialogs.Characters;

public partial class Delete
{
    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [Inject] private IState<CharacterState> CharacterState { get; set; } = null!;

    [Inject] private ICharacterStateProvider CharacterStateProvider { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        CharacterStateProvider.DeleteSelectedCharacter(Id);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
