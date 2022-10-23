using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Client.Helpers;

namespace Tracker.Client.Shared.Dialogs.Characters;

public partial class Update
{
    private readonly UpdateCharacterRequest _request = new();

    private bool _isLoading;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public CharacterResponse Character { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    protected override void OnInitialized()
    {
        _request.Name = Character.Name;
        _request.Class = Character.Class;
        _request.FirstProfession = Character.FirstProfession;
        _request.SecondProfession = Character.SecondProfession;
        _request.HasCooking = Character.HasCooking;
    }

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        _isLoading = true;

        //var response = await CharacterManager.UpdateAsync(Character.Id, _request);

        //if (response.Succeeded)
        //{
        //    await AppStateProvider.UpdateCharactersAsync();

        //    _isLoading = false;

        //    ToastService.ShowSuccess($"{_request.Name} has been updated");
        //}
        //else
        //{
        //    response.ToastError(ToastService);
        //}

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
