using System.Windows;
using Tracker.UI.Core;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.Views;

/// <summary>Interaction logic for ShellView.xaml</summary>
public partial class ShellView : Window
{
    public ShellView()
    {
        InitializeComponent();

        DataContext = IoC.Get<ShellViewModel>();
    }
}
