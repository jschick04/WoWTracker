using System;
using System.Threading.Tasks;
using MudBlazor;
using Tracker.Client.Core.Settings;
using Tracker.Client.Shared.Dialogs;
using Tracker.Library.Helpers;

namespace Tracker.Client.Shared {

    public partial class MainLayout : IDisposable {

        private MudTheme _currentTheme;
        private bool _drawerOpen = true;
        private bool _isDarkMode;

        public void Dispose() {
            _interceptor.DisposeEvent();
        }

        protected override Task OnInitializedAsync() {
            _currentTheme = TrackerTheme.DefaultTheme;
            _interceptor.RegisterEvent();

            return Task.CompletedTask;
        }

        private void DrawerToggle() => _drawerOpen = !_drawerOpen;

        private void Logout() {
            var parameters = new DialogParameters {
                { nameof(Dialogs.Logout.ContextText), "Logout Confirmation" },
                { nameof(Dialogs.Logout.ButtonText), "Logout" },
                { nameof(Dialogs.Logout.Color), Color.Error }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

            _dialogService.Show<Logout>("Logout", parameters, options);
        }

        private void ToggleDarkMode() =>
            _currentTheme = _isDarkMode ? TrackerTheme.DefaultTheme : TrackerTheme.DarkTheme;

    }

}