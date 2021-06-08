namespace TrackerApi.Contracts.V1 {

    public class ApiRoutes {

        public const string Base = Root + "/" + Version;
        public const string Root = "api";
        public const string Version = "v1";

        public static class Character {

            public const string Create = Base + "/character";
            public const string Delete = Base + "/character/{id}";
            public const string GetAll = Base + "/character";
            public const string GetById = Base + "/character/{id}";
            public const string Update = Base + "/character/{id}";

        }

        public static class Identity {

            public const string Authenticate = Base + "/identity/authenticate";
            public const string Delete = Base + "/identity/{id}";
            public const string GetAll = Base + "/identity";
            public const string GetById = Base + "/identity/{id}";
            public const string Register = Base + "/identity/register";
            public const string Update = Base + "/identity/{id}";
            public const string VerifyEmail = Base + "/identity/verify";

            #region Passwords

            public const string ForgotPassword = Base + "/identity/forgot";
            public const string ResetPassword = Base + "/identity/reset";

            #endregion

            #region Tokens

            public const string RefreshToken = Base + "/identity/token/refresh";
            public const string RevokeToken = Base + "/identity/token/revoke";
            public const string ValidateToken = Base + "/identity/token/validate";

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