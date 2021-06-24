namespace Tracker.Api.Contracts.Routes {

    public static class ClientRoutes {

        public static class Account {

            public const string Verify = Root + "/verify";

            private const string Root = "account";

            #region Passwords

            public const string ForgotPassword = Root + "/forgot";
            public const string ResetPassword = Root + "/reset";

            #endregion

        }

    }

}