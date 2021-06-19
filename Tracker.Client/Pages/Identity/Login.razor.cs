using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;

namespace Tracker.Client.Pages.Identity {

    public partial class Login {

        private readonly AuthenticationRequest _request = new();

        private string _passwordIcon = Icons.Material.Filled.VisibilityOff;
        private InputType _passwordInput = InputType.Password;
        private bool _passwordVisibility;

        protected override async Task OnInitializedAsync() {
            await _stateProvider.GetAuthenticationStateAsync();

            if (!_stateProvider.IsAnonymous) {
                _navigationManager.NavigateTo("/");
            }
        }

        private async Task SubmitAsync() {
            var result = await _authenticationManager.Login(_request);

            if (result.Succeeded) {
                _navigationManager.NavigateTo("/", true);
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
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