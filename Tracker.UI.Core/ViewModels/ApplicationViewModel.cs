using Tracker.UI.Core.Helpers;

namespace Tracker.UI.Core.ViewModels;

public class ApplicationViewModel : BaseViewModel
{
    private ApplicationPage _currentPage = ApplicationPage.Login;
    private bool _sideMenuVisible;

    public ApplicationPage CurrentPage
    {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    public bool SideMenuVisible
    {
        get => _sideMenuVisible;
        set => SetProperty(ref _sideMenuVisible, value);
    }

    public void GoToPage(ApplicationPage page)
    {
        CurrentPage = page;

        SideMenuVisible = page == ApplicationPage.Summary;
    }
}
