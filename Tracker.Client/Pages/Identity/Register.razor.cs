using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Client.Shared.Base;

namespace Tracker.Client.Pages.Identity {

    public partial class Register : PasswordForm {

        private readonly RegistrationRequest _request = new();

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

    }

}