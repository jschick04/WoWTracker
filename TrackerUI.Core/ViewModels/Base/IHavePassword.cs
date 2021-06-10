using System.Security;

namespace TrackerUI.Core.ViewModels {

    public interface IHavePassword {

        SecureString SecurePassword { get; }

    }

}