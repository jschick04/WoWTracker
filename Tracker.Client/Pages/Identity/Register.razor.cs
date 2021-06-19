using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Pages.Identity {

    public partial class Register {

        private readonly RegistrationRequest _request = new();

        private string _confirmIcon = Icons.Material.Filled.VisibilityOff;
        private InputType _confirmInput = InputType.Password;
        private bool _confirmVisibility;

        private string _passwordIcon = Icons.Material.Filled.VisibilityOff;
        private InputType _passwordInput = InputType.Password;
        private bool _passwordVisibility;

        private async Task SubmitAsync() {
            var result = await _userManager.RegisterAsync(_request);

            if (result.Succeeded) {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Info);
                }

                _navigationManager.NavigateTo("/");
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

        private void ToggleConfirmVisibility() {
            if (_confirmVisibility) {
                _confirmVisibility = false;
                _confirmIcon = Icons.Material.Filled.VisibilityOff;
                _confirmInput = InputType.Password;
            } else {
                _confirmVisibility = true;
                _confirmIcon = Icons.Material.Filled.Visibility;
                _confirmInput = InputType.Text;
            }
        }

        private void TogglePasswordVisibility() {
            if (_passwordVisibility) {
                _passwordVisibility = false;
                _passwordIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            } else {
                _passwordVisibility = true;
                _passwordIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }

    }

}