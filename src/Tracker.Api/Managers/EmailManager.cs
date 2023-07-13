using System.Text;
using FluentEmail.Core;
using Hangfire;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Entities;

namespace Tracker.Api.Managers;

public class EmailManager : IEmailManager
{
    private readonly IFluentEmail _sender;

    public EmailManager(IFluentEmail sender) => _sender = sender;

    public void SendAlreadyRegistered(User user, string origin)
    {
        var builder = new StringBuilder();

        builder.AppendLine("<h4>Your account has already been created.</h4>");

        if (!string.IsNullOrEmpty(origin))
        {
            var verifyUri = $"{origin}/{ClientRoutes.Account.VerifyUri}?token={user.VerificationToken}";

            builder.AppendLine("<p>Please click the below link to confirm you account.</p>");
            builder.AppendFormat("<p><a href=\"{0}\">Verify</a></p>", verifyUri).AppendLine();
        }
        else
        {
            builder.AppendLine("<p>Please use the following API to confirm your account.</p>");
            builder.AppendFormat("<p>API Route: {0}</p>", ApiRoutes.Account.VerifyEmailUri).AppendLine();
            builder.AppendFormat("<p>Token: <code>{0}</code></p>", user.VerificationToken).AppendLine();
        }

        builder.AppendLine("<p>- WoW Tracker Team</p>");

        BackgroundJob.Enqueue(() => SendAsync(user.Username, "Confirm Registration", builder.ToString(), null!));
    }

    public async Task SendAsync(string to, string subject, string html, string from = null!)
    {
        await _sender.To(to).Subject(subject).Body(html, true).SendAsync();
    }

    public void SendForgotPassword(User user, string? origin)
    {
        var builder = new StringBuilder();

        builder.AppendLine("<h4>A request to reset your password has been submitted.</h4>");

        if (!string.IsNullOrEmpty(origin))
        {
            var resetUri = $"{origin}/{ClientRoutes.Account.ResetPasswordUri}?token={user.ResetToken}";

            builder.AppendLine("<p>Please click the below link to reset your password.</p>");
            builder.AppendFormat("<p><a href=\"{0}\">Reset</a></p>", resetUri).AppendLine();
        }
        else
        {
            builder.AppendLine("<p>Please use the following API to reset your password</p>");
            builder.AppendFormat("<p>API Route: {0}</p>", ApiRoutes.Account.ResetPasswordUri).AppendLine();
            builder.AppendFormat("<p>Token: <code>{0}</code></p>", user.ResetToken).AppendLine();
        }

        builder.AppendLine("<p>- WoW Tracker Team</p>");

        BackgroundJob.Enqueue(() => SendAsync(user.Username, "Forgot Password", builder.ToString(), null!));
    }

    public void SendVerification(User user, string? origin)
    {
        var builder = new StringBuilder();

        builder.AppendLine("<h4>Thanks for registering!</h4>");

        if (!string.IsNullOrEmpty(origin))
        {
            var verifyUri = $"{origin}/{ClientRoutes.Account.VerifyUri}?token={user.VerificationToken}";

            builder.AppendLine("<p>Please click the below link to verify your email address.</p>");
            builder.AppendFormat("<p><a href=\"{0}\">Verify</a></p>", verifyUri).AppendLine();
        }
        else
        {
            builder.AppendLine("<p>Please use the following API to verify your email</p>");
            builder.AppendFormat("<p>API Route: {0}</p>", ApiRoutes.Account.VerifyEmailUri).AppendLine();
            builder.AppendFormat("<p>Token: <code>{0}</code></p>", user.VerificationToken).AppendLine();
        }

        builder.AppendLine("<p>- WoW Tracker Team</p>");

        BackgroundJob.Enqueue(() => SendAsync(user.Username, "Confirm Registration", builder.ToString(), null!));
    }
}
