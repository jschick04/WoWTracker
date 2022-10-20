using System.Security;

namespace Tracker.UI.Core.ViewModels;

public interface IHavePassword
{
    SecureString SecurePassword { get; }
}
