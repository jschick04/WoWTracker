﻿using System.Threading.Tasks;
using System.Windows.Input;
using TrackerUI.Helpers;
using TrackerUI.Views;

namespace TrackerUI.ViewModels {

    public class LoginViewModel : BaseViewModel {

        private bool _isLoggingIn;

        private ICommand _logInCommand;
        private string _userName;

        public bool CanLogIn => !IsLoggingIn;

        public bool IsLoggingIn {
            get => _isLoggingIn;
            set {
                SetProperty(ref _isLoggingIn, value);
                OnPropertyChanged(nameof(CanLogIn));
            }
        }

        public ICommand LogInCommand =>
            _logInCommand ??= new RelayParameterizedCommand(async parameter => await LogIn(parameter));

        public string UserName {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public async Task LogIn(object parameter) {
            await RunCommand(
                () => IsLoggingIn,
                async () => {
                    await Task.Delay(5000);

                    var email = UserName;
                    var pass = (parameter as IHavePassword)?.SecurePassword.Unsecure();
                }
            );
        }

    }

}