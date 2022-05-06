using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace Tracker.Client.Shared.Dialogs;

public partial class Logout {

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit() {
        await AuthenticationManager.Logout();

        ToastService.ShowSuccess("You are now logged out");
        NavigationManager.NavigateTo("/");

        await Modal.CloseAsync(ModalResult.Ok(true));
    }

}