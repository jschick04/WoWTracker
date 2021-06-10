using System.Windows;
using TrackerUI.Core;
using TrackerUI.Views;

namespace TrackerUI {

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