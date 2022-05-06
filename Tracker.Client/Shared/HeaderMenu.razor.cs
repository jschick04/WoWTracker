using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.Client.Helpers;
using Tracker.Client.Shared.Dialogs;

namespace Tracker.Client.Shared;

public partial class HeaderMenu {

    [Parameter] public bool DrawerOpen { get; set; }

    [Parameter] public EventCallback<bool> DrawerOpenChanged { get; set; }

    [CascadingParameter] protected bool IsDarkMode { get; set; }

    private async Task DrawerToggle() {
        DrawerOpen = !DrawerOpen;
        await DrawerOpenChanged.InvokeAsync(DrawerOpen);
    }

    private void LogOut() {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Logout.ContextText), "Are you sure you want to logout?");
        parameters.Add(nameof(Logout.ButtonText), "Logout");

        DialogService.Show<Logout>("Logout Confirmation", parameters, options);
    }

}