using System.Net.Http.Json;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Routes;

namespace Tracker.UI.Pages.Account;

public partial class ForgotPassword
{
    private readonly ForgotPasswordRequest _request = new();

    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    private async Task SubmitAsync()
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync(ApiRoutes.Account.ForgotPasswordUri, _request);

            response.EnsureSuccessStatusCode();

            var message = await response.Content.ReadAsStringAsync() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Account.ForgotPasswordUri}");

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
