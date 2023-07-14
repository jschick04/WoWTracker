using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.Routes;
using Tracker.UI.Helpers;

namespace Tracker.UI.Pages.Account;

public partial class VerifyToken
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var token = NavigationManager.QueryString("token");

        if (string.IsNullOrWhiteSpace(token))
        {
            ToastService.ShowError("Invalid Token");
            return;
        }

        try
        {
            var response = await HttpClient.PostAsync($"{ApiRoutes.Account.VerifyEmailUri}?token={token}", null);

            response.EnsureSuccessStatusCode();

            ToastService.ShowSuccess("Account successfully verified");
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
        finally
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
