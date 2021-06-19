namespace Tracker.Api.Contracts.V1 {

    public class ApiRoutes {

        public const string Base = Root + "/" + Version;
        public const string Root = "api";
        public const string Version = "v1";

        public static class Account {

            public const string Delete = Root + "/account/{id}";
            public const string GetAll = Root + "/account";
            public const string GetById = Root + "/account/{id}";
            public const string Update = Root + "/account/{id}";
            public const string VerifyEmail = Root + "/account/verify";

            #region Passwords

            public const string ForgotPassword = Root + "/account/forgot";
            public const string ResetPassword = Root + "/account/reset";

            #endregion

        }

        public static class Character {

            public const string Create = Base + "/character";
            public const string Delete = Base + "/character/{id}";
            public const string GetAll = Base + "/character";
            public const string GetById = Base + "/character/{id}";
            public const string Update = Base + "/character/{id}";

        }

        public static class Identity {

            public const string Authenticate = Root + "/identity/authenticate";
            public const string Register = Root + "/identity/register";

            #region Tokens

            public const string RefreshToken = Root + "/identity/token/refresh";
            public const string RevokeToken = Root + "/identity/token/revoke";
            public const string ValidateToken = Root + "/identity/token/validate";

            #endregion

        }

        public static class Item {

            public const string GetByProfession = Base + "/item/profession/{profession}";
            public const string GetBySlot = Base + "/item/slot/{name}";

        }

        public static class Profession {

            public const string GetAll = Base + "/profession";

        }

    }

}