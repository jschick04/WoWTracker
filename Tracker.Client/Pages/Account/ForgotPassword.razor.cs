using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Client.Helpers;

namespace Tracker.Client.Pages.Account;

public partial class ForgotPassword {

    private readonly ForgotPasswordRequest _request = new();

    private async Task SubmitAsync() {
        var result = await UserManager.ForgotPasswordAsync(_request);

        result.ToastMessage(ToastService);

        NavigationManager.NavigateTo("/");
    }

}