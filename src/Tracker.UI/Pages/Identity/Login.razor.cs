using Tracker.Api.Contracts.Identity.Requests;
using Tracker.UI.Helpers;

namespace Tracker.UI.Pages.Identity;

public partial class Login
{
    private readonly AuthenticationRequest _request = new();

    private bool _isLoading;

    protected override async Task OnInitializedAsync()
    {
        await ClientAuthStateProvider.GetAuthenticationStateAsync();

        if (!ClientAuthStateProvider.IsAnonymous)
        {
            NavigationManager.NavigateTo("/");
        }
    }

    private async Task SubmitAsync()
    {
        _isLoading = true;

        var result = await AuthenticationManager.Login(_request);

        _isLoading = false;

        if (result.Succeeded)
        {
            ToastService.ShowSuccess("You are now logged in");
            NavigationManager.NavigateTo("/");
        }
        else
        {
            result.ToastError(ToastService);
        }
    }
}
