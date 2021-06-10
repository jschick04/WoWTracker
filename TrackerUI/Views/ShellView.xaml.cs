using System.Windows;
using TrackerUI.Core;
using TrackerUI.Core.ViewModels;

namespace TrackerUI.Views {

    /// <summary>Interaction logic for ShellView.xaml</summary>
    public partial class ShellView : Window {

        public ShellView() {
            InitializeComponent();

            DataContext = IoC.Get<ShellViewModel>();
        }

    }

}