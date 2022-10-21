using Tracker.Client.Library.Store.Character;

namespace Tracker.Client.Shared;

public partial class MainLayout : IDisposable
{
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

        await base.OnInitializedAsync();
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        await ItemManager.GetAllAsync();

        var user = await StateProvider.GetAuthenticationStateProviderUserAsync();

        if (user.Identity?.IsAuthenticated is not true) { return; }

        Dispatcher.Dispatch(new FetchDataAction());
    }
}
