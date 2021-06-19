using System.Windows;
using Tracker.UI.Core;
using Tracker.UI.Views;

namespace Tracker.UI {

    /// <summary>Interaction logic for App.xaml</summary>
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            IoC.Initialize();

            Current.MainWindow = new ShellView();
            Current.MainWindow.Show();
        }

    }

}