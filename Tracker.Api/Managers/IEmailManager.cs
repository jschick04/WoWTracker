using Tracker.Api.Entities;

namespace Tracker.Api.Managers;

public interface IEmailManager {

    void SendAlreadyRegistered(User user, string origin);

    Task SendAsync(string to, string subject, string html, string from = null!);

    void SendForgotPassword(User user, string origin);

    void SendVerification(User user, string origin);

}