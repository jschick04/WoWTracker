namespace Tracker.Api.Contracts.Routes;

public static class ApiRoutes
{
    private const string Version = "v1";

    public static class Account
    {
        public const string Delete = Uri + "/{id}";
        public const string GetAll = Uri;
        public const string GetById = Uri + "/{id}";
        public const string Update = Uri + "/{id}";
        public const string Uri = Root;
        public const string VerifyEmail = Uri + "/verify";

        private const string Root = "account";

        public static string DeleteReplace(int id) => $"{Uri}/{id}";

        public static string GetByIdReplace(int id) => $"{Uri}/{id}";

        public static string UpdateReplace(int id) => $"{Uri}/{id}";

#region Passwords

        public const string ForgotPassword = Uri + "/forgot";
        public const string ResetPassword = Uri + "/reset";

#endregion
    }

    public static class Character
    {
        public const string AddNeededItem = Uri + "/{id}/needed/add";
        public const string Create = Uri;
        public const string Delete = Uri + "/{id}";
        public const string GetAll = Uri;
        public const string GetById = Uri + "/{id}";
        public const string GetNeededItems = Uri + "/{id}/needed";
        public const string RemoveNeededItem = Uri + "/{id}/needed/remove";
        public const string Update = Uri + "/{id}";
        public const string Uri = Version + "/" + Root;

        private const string Root = "character";

        public static string AddNeededItemReplace(int id) => $"{Uri}/{id}/needed/add";

        public static string DeleteReplace(int id) => $"{Uri}/{id}";

        public static string GetByIdReplace(int id) => $"{Uri}/{id}";

        public static string GetNeededItemsReplace(int id) => $"{Uri}/{id}/needed";

        public static string RemoveNeededItemsReplace(int id) => $"{Uri}/{id}/needed/remove";

        public static string UpdateReplace(int id) => $"{Uri}/{id}";
    }

    public static class Identity
    {
        public const string Authenticate = Uri + "/authenticate";
        public const string Register = Uri + "/register";
        public const string Uri = Root;

        private const string Root = "identity";

#region Tokens

        public const string RefreshToken = Uri + "/token/refresh";
        public const string RevokeToken = Uri + "/token/revoke";
        public const string ValidateToken = Uri + "/token/validate";

#endregion
    }

    public static class Item
    {
        public const string GetAll = Uri;
        public const string GetByProfession = Uri + "/profession/{name}";
        public const string GetBySlot = Uri + "/slot/{name}";
        public const string GetCraftableByProfession = Uri + "/craftable/{name}";
        public const string Uri = Version + "/" + Root;

        private const string Root = "item";

        public static string GetByProfessionReplace(string name) => $"{Uri}/profession/{name}";

        public static string GetBySlotReplace(string name) => $"{Uri}/slot/{name}";

        public static string GetCraftableByProfessionReplace(string name) => $"{Uri}/craftable/{name}";
    }

    public static class Profession
    {
        public const string GetAll = Uri;
        public const string Uri = Version + "/" + Root;

        private const string Root = "profession";
    }
}
