using Tracker.Api.Contracts.Identity.Requests;
using Tracker.UI.Helpers;

namespace Tracker.UI.Pages.Identity;

public partial class Register
{
    private readonly RegistrationRequest _request = new();
    private bool _isLoading;

    private async Task SubmitAsync()
    {
        _isLoading = true;

        var result = await UserManager.RegisterAsync(_request);
        result.ToastMessage(ToastService);

        _isLoading = false;

        if (result.Succeeded)
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
