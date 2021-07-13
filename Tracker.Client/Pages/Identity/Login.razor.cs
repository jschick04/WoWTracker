using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Client.Shared.Base;

namespace Tracker.Client.Pages.Identity {

    public partial class Login : PasswordForm {

        private readonly AuthenticationRequest _request = new();

        protected override async Task OnInitializedAsync() {
            await _stateProvider.GetAuthenticationStateAsync();

            if (!_stateProvider.IsAnonymous) {
                _navigationManager.NavigateTo("/");
            }
        }

        private async Task SubmitAsync() {
            var result = await _authenticationManager.Login(_request);

            if (result.Succeeded) {
                _navigationManager.NavigateTo("/");
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }

    }

}