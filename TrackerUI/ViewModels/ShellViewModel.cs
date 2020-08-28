using System;
using System.Windows.Input;
using TrackerUI.Helpers;

namespace TrackerUI.ViewModels {

    public class ShellViewModel : BaseViewModel {

        private ApplicationPage _currentPage = ApplicationPage.Summary;
        private ICommand _exitCommand;
        private ICommand _logInCommand;

        public ApplicationPage CurrentPage {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public ICommand ExitCommand => _exitCommand ??= new RelayCommand(ExitApplication);

        public ICommand LogInCommand => _logInCommand ??= new RelayCommand(OpenLogInPage);

        public void ExitApplication() {
            Environment.Exit(0);
        }

        public void OpenLogInPage() {
            CurrentPage = ApplicationPage.Login;
        }

    }

}