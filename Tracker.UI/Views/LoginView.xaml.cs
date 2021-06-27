using System.Security;
using Tracker.UI.Core.ViewModels;

namespace Tracker.UI.Views {

    /// <summary>Interaction logic for LoginView.xaml</summary>
    public partial class LoginView : BaseView<LoginViewModel>, IHavePassword {

        public LoginView() {
            InitializeComponent();
        }

        public SecureString SecurePassword => Password.SecurePassword;

    }

}