using Tracker.Api.Contracts.V1;
using Tracker.Api.Entities;
using Tracker.Api.Settings;

namespace Tracker.Api.Managers {

    public class EmailManager : IEmailManager {

        private readonly EmailSettings _settings;

        public EmailManager(EmailSettings settings) => _settings = settings;

        public void SendAlreadyRegisteredEmail(string username, string origin) {
            // TODO: Implement
        }

        public void SendPasswordResetEmail(User user, string origin) {
            // TODO: Implement
        }

        public void SendVerificationEmail(User user, string origin) {
            string message;

            if (!string.IsNullOrEmpty(origin)) {
                var verifyUrl = $"{origin}{ApiRoutes.Account.VerifyEmail}?token={user.VerificationToken}";

                message = $@"<p>Please click the below link to verify your email address.</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            } else {
                message =
                    $@"<p>Please use the below token to verify your email with the <code>{ApiRoutes.Account.VerifyEmail}</code> api route.</p>
                       <p><code>{user.VerificationToken}</code></p>";
            }

            Send(
                user.Username,
                "WoW Tracker Email Verification",
                $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        private void Send(string to, string subject, string html, string from = null) {
            // TODO: Implement
        }

    }

}