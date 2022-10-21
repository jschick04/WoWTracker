using Tracker.UI.Core;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.ViewModels;

public class ViewModelLocator
{
    public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

    public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();
}
