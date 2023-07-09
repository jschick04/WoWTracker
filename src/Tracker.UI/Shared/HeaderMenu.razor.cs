using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Helpers;
using Tracker.Client.Library.Features.NavMenu;
using Tracker.UI.Shared.Dialogs;

namespace Tracker.UI.Shared;

public partial class HeaderMenu
{
    [CascadingParameter] protected bool IsDarkMode { get; set; }

    private void DrawerToggle() => Dispatcher.Dispatch(new NavMenuToggleDrawerAction());

    private void LogOut()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Logout.ContextText), "Are you sure you want to logout?");
        parameters.Add(nameof(Logout.ButtonText), "Logout");

        DialogService.Show<Logout>("Logout Confirmation", parameters, options);
    }
}
