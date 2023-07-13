using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.UI.Library.Managers.Authentication;

namespace Tracker.UI.Pages.Identity;

public partial class Login
{
    private readonly AuthenticationRequest _request = new();

    private bool _isLoading;

    [Inject] private IAuthenticationManager AuthenticationManager { get; set; } = null!;

    [Inject] private ClientAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await AuthenticationStateProvider.GetAuthenticationStateAsync();

        if (!AuthenticationStateProvider.IsAnonymous)
        {
            NavigationManager.NavigateTo("/");
        }
    }

    private async Task SubmitAsync()
    {
        _isLoading = true;

        var result = await AuthenticationManager.Login(_request);

        _isLoading = false;

        if (result.IsSuccess)
        {
            ToastService.ShowSuccess("You are now logged in");
            NavigationManager.NavigateTo("/");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ToastService.ShowError(error.Message);
            }
        }
    }
}
