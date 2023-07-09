using Tracker.Api.Contracts.Identity.Requests;
using Tracker.UI.Helpers;
using Tracker.Library.Helpers;

namespace Tracker.UI.Pages.Account;

public partial class ResetPassword
{
    private readonly ResetPasswordRequest _request = new();

    protected override void OnInitialized()
    {
        _request.Token = NavigationManager.QueryString("token") ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(_request.Token))
        { return; }

        ToastService.ShowError("Invalid Token");
        NavigationManager.NavigateTo("/");
    }

    private async Task SubmitAsync()
    {
        var result = await UserManager.ResetPasswordAsync(_request);

        result.ToastMessage(ToastService);

        NavigationManager.NavigateTo("/");
    }
}
