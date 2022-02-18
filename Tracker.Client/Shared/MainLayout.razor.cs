using Tracker.Client.Library.Settings;
using Tracker.Client.Shared.Dialogs;
using Tracker.Library.Helpers;

namespace Tracker.Client.Shared;

public partial class MainLayout : IDisposable {

    private MudTheme _currentTheme = null!;
    private bool _drawerOpen = true;
    private bool _isDarkMode;

    public void Dispose() {
        _interceptor.DisposeEvent();
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync() {
        // TODO: Pull from settings
        _currentTheme = TrackerTheme.DefaultTheme;

        _interceptor.RegisterEvent();

        await LoadDataAsync();
    }

    private void DrawerToggle() => _drawerOpen = !_drawerOpen;

    private async Task LoadDataAsync() {
        var user = await _stateProvider.GetAuthenticationStateProviderUserAsync();

        if (user?.Identity?.IsAuthenticated is not true) { return; }

        var result = await _userManager.GetAsync(user.GetId());

        if (!result.Succeeded || result?.Data is null) {
            _snackbar.Add("You are not Logged In", Severity.Error);
            await _authenticationManager.Logout();
        }
    }

    private void Logout() {
        var parameters = new DialogParameters {
            { nameof(Dialogs.Logout.ContextText), "Logout Confirmation" },
            { nameof(Dialogs.Logout.ButtonText), "Logout" },
            { nameof(Dialogs.Logout.Color), Color.Error }
        };

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

        _dialogService.Show<Logout>("Logout", parameters, options);
    }

    private void ToggleDarkMode() => _currentTheme = _isDarkMode ? TrackerTheme.DefaultTheme : TrackerTheme.DarkTheme;

}