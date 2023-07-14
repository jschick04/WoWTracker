using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.UI.Library.Features.State;
using Tracker.UI.Library.StateProviders;

namespace Tracker.UI.Shared.Dialogs.Characters;

public partial class Update
{
    private readonly UpdateCharacterRequest _request = new();

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public CharacterResponse Character { get; set; } = null!;

    [Inject] private IState<CharacterState> CharacterState { get; set; } = null!;

    [Inject] private ICharacterStateProvider CharacterStateProvider { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _request.Name = Character.Name;
        _request.Class = Character.Class;
        _request.FirstProfession = Character.FirstProfession;
        _request.SecondProfession = Character.SecondProfession;
        _request.HasCooking = Character.HasCooking;
    }

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        CharacterStateProvider.UpdateSelectedCharacter(Character.Id, _request);

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
