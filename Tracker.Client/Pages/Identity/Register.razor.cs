using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Client.Shared.Base;

namespace Tracker.Client.Pages.Identity {

    public partial class Register : PasswordForm {

        private readonly RegistrationRequest _request = new();

        private async Task SubmitAsync() {
            isLoading = true;

            var result = await _userManager.RegisterAsync(_request);

            isLoading = false;

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