using Tracker.Api.Entities;

namespace Tracker.Api.Managers {

    public interface IEmailManager {

        void SendAlreadyRegisteredEmail(string username, string origin);

        void SendPasswordResetEmail(User user, string origin);

        void SendVerificationEmail(User user, string origin);

    }

}