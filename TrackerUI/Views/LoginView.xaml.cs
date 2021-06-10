using System.Security;
using TrackerUI.Core.ViewModels;

namespace TrackerUI.Views {

    /// <summary>Interaction logic for LoginView.xaml</summary>
    public partial class LoginView : BaseView<LoginViewModel>, IHavePassword {

        public LoginView() {
            InitializeComponent();
        }

        public SecureString SecurePassword => Password.SecurePassword;

    }

}