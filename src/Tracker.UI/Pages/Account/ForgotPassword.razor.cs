using Tracker.Api.Contracts.Identity.Requests;
using Tracker.UI.Helpers;

namespace Tracker.UI.Pages.Account;

public partial class ForgotPassword
{
    private readonly ForgotPasswordRequest _request = new();

    private async Task SubmitAsync()
    {
        var result = await UserManager.ForgotPasswordAsync(_request);

        result.ToastMessage(ToastService);

        NavigationManager.NavigateTo("/");
    }
}
