using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Client.Shared.Base;
using Tracker.Library.Helpers;

namespace Tracker.Client.Pages.Account;

public partial class ResetPassword : PasswordForm {

    private readonly ResetPasswordRequest _request = new();

    protected override void OnInitialized() {
        _request.Token = _navigationManager.QueryString("token");

        if (!string.IsNullOrWhiteSpace(_request.Token)) { return; }

        _snackbar.Add("Invalid Token", Severity.Error);
        _navigationManager.NavigateTo("/");
    }

    private async Task SubmitAsync() {
        var result = await _userManager.ResetPasswordAsync(_request);

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