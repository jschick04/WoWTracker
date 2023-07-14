using Blazored.Modal;
using Blazored.Modal.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.UI.Helpers;
using Tracker.UI.Library.Features.NavMenu;
using Tracker.UI.Shared.Dialogs;

namespace Tracker.UI.Shared;

public partial class HeaderMenu
{
    [CascadingParameter] protected bool IsDarkMode { get; set; }

    [Inject] private IDispatcher Dispatcher { get; set; } = null!;

    [Inject] private IModalService ModalService { get; set; } = null!;

    private void DrawerToggle() => Dispatcher.Dispatch(new NavMenuToggleDrawerAction());

    private void LogOut()
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions().GetClass(IsDarkMode, true);

        parameters.Add(nameof(Logout.ContextText), "Are you sure you want to logout?");
        parameters.Add(nameof(Logout.ButtonText), "Logout");

        ModalService.Show<Logout>("Logout Confirmation", parameters, options);
    }
}
