using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.Client.Helpers;
using Tracker.Client.Library.Store.NavMenu;
using Tracker.Client.Shared.Dialogs;

namespace Tracker.Client.Shared;

public partial class HeaderMenu
{
    [CascadingParameter] protected bool IsDarkMode { get; set; }

    private void DrawerToggle() => Dispatcher.Dispatch(new ToggleDrawerOpenAction());

    private void LogOut()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Logout.ContextText), "Are you sure you want to logout?");
        parameters.Add(nameof(Logout.ButtonText), "Logout");

        DialogService.Show<Logout>("Logout Confirmation", parameters, options);
    }
}
