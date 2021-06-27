namespace Tracker.Api.Contracts.Routes {

    public static class ApiRoutes {

        private const string RootUri = Uri + "/" + Version;
        private const string Uri = "api";
        private const string Version = "v1";

        public static class Account {

            public const string Delete = PathUri + "/{id}";
            public const string GetAll = PathUri;
            public const string GetById = PathUri + "/{id}";
            public const string PathUri = Uri + "/" + Root;
            public const string Update = PathUri + "/{id}";
            public const string VerifyEmail = PathUri + "/verify";

            private const string Root = "account";

            public static string DeleteReplace(int id) => $"{PathUri}/{id}";

            public static string GetByIdReplace(int id) => $"{PathUri}/{id}";

            public static string UpdateReplace(int id) => $"{PathUri}/{id}";

            #region Passwords

            public const string ForgotPassword = PathUri + "/forgot";
            public const string ResetPassword = PathUri + "/reset";

            #endregion

        }

        public static class Character {

            public const string Create = PathUri;
            public const string Delete = PathUri + "/{id}";
            public const string GetAll = PathUri;
            public const string GetById = PathUri + "/{id}";
            public const string PathUri = RootUri + "/" + Root;
            public const string Update = PathUri + "/{id}";

            private const string Root = "character";

            public static string DeleteReplace(int id) => $"{PathUri}/{id}";

            public static string GetByIdReplace(int id) => $"{PathUri}/{id}";

            public static string UpdateReplace(int id) => $"{PathUri}/{id}";

        }

        public static class Identity {

            public const string Authenticate = PathUri + "/authenticate";
            public const string PathUri = Uri + "/" + Root;
            public const string Register = PathUri + "/register";

            private const string Root = "identity";

            #region Tokens

            public const string RefreshToken = PathUri + "/token/refresh";
            public const string RevokeToken = PathUri + "/token/revoke";
            public const string ValidateToken = PathUri + "/token/validate";

            #endregion

        }

        public static class Item {

            public const string GetByProfession = PathUri + "/profession/{name}";
            public const string GetBySlot = PathUri + "/slot/{name}";
            public const string PathUri = RootUri + "/" + Root;

            private const string Root = "item";

            public static string GetByProfessionReplace(string name) => $"{PathUri}/profession/{name}";

            public static string GetBySlotReplace(string name) => $"{PathUri}/slot/{name}";

        }

        public static class Profession {

            public const string GetAll = PathUri;
            public const string PathUri = RootUri + "/" + Root;

            private const string Root = "profession";

        }

    }

}