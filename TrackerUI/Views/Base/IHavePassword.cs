using System.Security;

namespace TrackerUI.Views {

    public interface IHavePassword {

        SecureString SecurePassword { get; }

    }

}