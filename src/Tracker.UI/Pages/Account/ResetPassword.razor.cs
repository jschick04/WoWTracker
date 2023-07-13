using System.Net.Http.Json;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Routes;
using Tracker.UI.Helpers;

namespace Tracker.UI.Pages.Account;

public partial class ResetPassword
{
    private readonly ResetPasswordRequest _request = new();

    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    protected override void OnInitialized()
    {
        _request.Token = NavigationManager.QueryString("token") ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(_request.Token)) { return; }

        ToastService.ShowError("Invalid Token");
        NavigationManager.NavigateTo("/");
    }

    private async Task SubmitAsync()
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync(ApiRoutes.Account.ResetPasswordUri, _request);

            response.EnsureSuccessStatusCode();

            var message = await response.Content.ReadAsStringAsync() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Account.ResetPasswordUri}");

            ToastService.ShowSuccess(message);
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
