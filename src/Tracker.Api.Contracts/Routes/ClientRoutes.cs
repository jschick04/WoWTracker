namespace Tracker.Api.Contracts.Routes;

public static class ClientRoutes
{
    public static class Account
    {
        public const string VerifyUri = Root + "/verify";

        private const string Root = "account";

#region Passwords

        public const string ForgotPasswordUri = Root + "/forgot";
        public const string ResetPasswordUri = Root + "/reset";

#endregion
    }
}
