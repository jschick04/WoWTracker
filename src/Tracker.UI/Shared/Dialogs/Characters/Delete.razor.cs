using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Helpers;

namespace Tracker.UI.Shared.Dialogs.Characters;

public partial class Delete
{
    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        CharacterStateProvider.DeleteSelectedCharacter(Id);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
