using System.Threading.Tasks;
using MudBlazor;
using Tracker.Api.Contracts.Identity.Requests;

namespace Tracker.Client.Pages.Account {

    public partial class ForgotPassword {

        private readonly ForgotPasswordRequest _request = new();

        private async Task SubmitAsync() {
            var result = await _userManager.ForgotPasswordAsync(_request);

            if (result.Succeeded) {
                _snackbar.Add("Password request has been sent", Severity.Success);
            } else {
                foreach (var message in result.Messages) {
                    _snackbar.Add(message, Severity.Error);
                }
            }

            _navigationManager.NavigateTo("/");
        }

    }

}