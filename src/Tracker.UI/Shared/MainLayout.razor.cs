using Blazored.Toast.Services;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Tracker.Api.Contracts.Routes;
using Tracker.Shared.Helpers;
using Tracker.UI.Library.Features.Profession;
using Tracker.UI.Library.Managers.Authentication;
using Tracker.UI.Library.Managers.Interceptors;

namespace Tracker.UI.Shared;

public partial class MainLayout : IDisposable
{
    private bool _isDarkMode;

    [Inject] private IAuthenticationManager AuthenticationManager { get; set; } = null!;

    [Inject] private ClientAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject] private IDispatcher Dispatcher { get; set; } = null!;

    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private IHttpInterceptorManager Interceptor { get; set; } = null!;

    [Inject] private IToastService ToastService { get; set; } = null!;

    public void Dispose()
    {
        Interceptor.DisposeEvent();
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync()
    {
        Interceptor.RegisterEvent();

        // TODO: Default to false, check user settings in LoadData
        _isDarkMode = false;

        await base.OnInitializedAsync();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        Dispatcher.Dispatch(new ProfessionGetAllItemsAction());

        var user = await AuthenticationStateProvider.GetAuthenticationStateProviderUserAsync();

        if (user.Identity?.IsAuthenticated is not true) { return; }

        try
        {
            var response = await HttpClient.GetAsync(ApiRoutes.Account.GetById(user.GetId()));

            response.EnsureSuccessStatusCode();
        }
        catch
        {
            ToastService.ShowError("You are not logged in");
            await AuthenticationManager.Logout();
        }
    }
}
