using TrackerApi.Entities;

namespace TrackerApi.Managers {

    public interface IEmailManager {

        void SendAlreadyRegisteredEmail(string username, string origin);

        void SendPasswordResetEmail(User user, string origin);

        void SendVerificationEmail(User user, string origin);

    }

}