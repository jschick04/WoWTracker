using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Client.Helpers;

namespace Tracker.Client.Shared.Dialogs.Characters;

public partial class Delete
{
    private bool _isLoading;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    [Parameter] public string Name { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        _isLoading = true;

        //var response = await CharacterManager.DeleteAsync(Id);

        //_isLoading = false;

        //if (response.Succeeded)
        //{
        //    ToastService.ShowSuccess($"{Name} has been deleted");
        //}
        //else
        //{
        //    response.ToastError(ToastService);
        //}

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
