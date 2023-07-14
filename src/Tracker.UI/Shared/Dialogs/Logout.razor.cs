using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Library.Managers.Authentication;

namespace Tracker.UI.Shared.Dialogs;

public partial class Logout
{
    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public string ContextText { get; set; } = null!;

    [Inject] private IAuthenticationManager AuthenticationManager { get; set; } = null!;

    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    private void Cancel() => Modal.CancelAsync();

    private async Task Submit()
    {
        await AuthenticationManager.Logout();

        ToastService.ShowSuccess("You are now logged out");
        NavigationManager.NavigateTo("/");

        await Modal.CloseAsync(ModalResult.Ok(true));
    }
}
