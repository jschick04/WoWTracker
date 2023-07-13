namespace Tracker.Api.Contracts.Routes;

public static class ApiRoutes
{
    private const string Version = "v1";

    public static class Account
    {
        public const string DeleteUri = Root + "/{id}";
        public const string GetAllUri = Root;
        public const string GetByIdUri = Root + "/{id}";
        public const string UpdateUri = Root + "/{id}";
        public const string VerifyEmailUri = Root + "/verify";

        private const string Root = "account";

        public static string Delete(int id) => $"{Root}/{id}";

        public static string GetById(int id) => $"{Root}/{id}";

        public static string Update(int id) => $"{Root}/{id}";

#region Passwords

        public const string ForgotPasswordUri = Root + "/forgot";
        public const string ResetPasswordUri = Root + "/reset";

#endregion
    }

    public static class Character
    {
        public const string AddNeededItemUri = Root + "/{id}/needed/add";
        public const string CreateUri = Root;
        public const string DeleteUri = Root + "/{id}";
        public const string GetAllUri = Root;
        public const string GetByIdUri = Root + "/{id}";
        public const string GetNeededItemsUri = Root + "/{id}/needed";
        public const string RemoveNeededItemUri = Root + "/{id}/needed/remove";
        public const string UpdateUri = Root + "/{id}";

        private const string Root = $"{Version}/character";

        public static string AddNeededItem(int id) => $"{Root}/{id}/needed/add";

        public static string Delete(int id) => $"{Root}/{id}";

        public static string GetById(int id) => $"{Root}/{id}";

        public static string GetNeededItems(int id) => $"{Root}/{id}/needed";

        public static string RemoveNeededItem(int id) => $"{Root}/{id}/needed/remove";

        public static string Update(int id) => $"{Root}/{id}";
    }

    public static class Identity
    {
        public const string AuthenticateUri = Uri + "/authenticate";
        public const string RegisterUri = Uri + "/register";

        public const string Uri = "identity";

#region Tokens

        public const string RefreshTokenUri = Uri + "/token/refresh";
        public const string RevokeTokenUri = Uri + "/token/revoke";
        public const string ValidateTokenUri = Uri + "/token/validate";

#endregion
    }

    public static class Item
    {
        public const string GetAllUri = Root;
        public const string GetByProfessionUri = Root + "/profession/{name}";
        public const string GetBySlotUri = Root + "/slot/{name}";
        public const string GetCraftableByProfessionUri = Root + "/craftable/{name}";

        private const string Root = $"{Version}/item";

        public static string GetByProfession(string name) => $"{Root}/profession/{name}";

        public static string GetBySlot(string name) => $"{Root}/slot/{name}";

        public static string GetCraftableByProfession(string name) => $"{Root}/craftable/{name}";
    }

    public static class Profession
    {
        public const string GetAllUri = Root;

        private const string Root = $"{Version}/profession";
    }
}
