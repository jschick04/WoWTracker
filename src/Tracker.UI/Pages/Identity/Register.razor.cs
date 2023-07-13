using System.Net.Http.Json;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Routes;

namespace Tracker.UI.Pages.Identity;

public partial class Register
{
    private readonly RegistrationRequest _request = new();

    private bool _isLoading;

    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    private async Task SubmitAsync()
    {
        _isLoading = true;

        try
        {
            var response = await HttpClient.PostAsJsonAsync(ApiRoutes.Identity.RegisterUri, _request);

            response.EnsureSuccessStatusCode();

            var message = await response.Content.ReadAsStringAsync() ??
                throw new Exception($"Error retrieving data from {ApiRoutes.Identity.RegisterUri}");

            ToastService.ShowSuccess(message);

            NavigationManager.NavigateTo("/");
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
        finally
        {
            _isLoading = false;
        }
    }
}
