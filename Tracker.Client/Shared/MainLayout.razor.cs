using Tracker.Library.Helpers;

namespace Tracker.Client.Shared;

public partial class MainLayout : IDisposable
{
    private bool _drawerOpen = true;
    private bool _isDarkMode;

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

        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        await ItemManager.GetAllAsync();

        var user = await StateProvider.GetAuthenticationStateProviderUserAsync();

        if (user.Identity?.IsAuthenticated is not true) { return; }

        var result = await UserManager.GetAsync(user.GetId());

        if (!result.Succeeded || result.Data is null)
        {
            ToastService.ShowError("You are not logged in");
            await AuthenticationManager.Logout();
        }
    }
}
