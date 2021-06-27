using System;
using System.Windows.Input;
using Tracker.UI.Core.Helpers;

namespace Tracker.UI.Core.ViewModels {

    public class ShellViewModel : BaseViewModel {

        private readonly ApplicationViewModel _app;

        private ICommand _exitCommand;
        private ICommand _logInCommand;
        private ICommand _summaryCommand;

        public ShellViewModel(ApplicationViewModel app) => _app = app;

        public ICommand ExitCommand => _exitCommand ??= new RelayCommand(ExitApplication);

        public ICommand LogInCommand => _logInCommand ??= new RelayCommand(OpenLogInPage);

        public ICommand SummaryCommand => _summaryCommand ??= new RelayCommand(OpenSummaryPage);

        public void ExitApplication() {
            Environment.Exit(0);
        }

        public void OpenLogInPage() {
            _app.GoToPage(ApplicationPage.Login);
        }

        public void OpenSummaryPage() {
            _app.GoToPage(ApplicationPage.Summary);
        }

    }

}