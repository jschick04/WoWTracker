using System.Reflection;
using System.Runtime.Serialization;

namespace Tracker.Api.Library.Helpers;

public enum Classes
{
    Warrior = 1,
    Paladin = 2,
    Hunter = 3,
    Rogue = 4,
    Priest = 5,
    Shaman = 6,
    Mage = 7,
    Warlock = 8,
    Monk = 9,
    Druid = 10,
    [EnumMember(Value = "Demon Hunter")] DemonHunter = 11,
    [EnumMember(Value = "Death Knight")] DeathKnight = 12
}

public enum Professions
{
    Alchemy = 1,
    Blacksmithing = 2,
    Enchanting = 3,
    Engineering = 4,
    Inscription = 5,
    Jewelcrafting = 6,
    Leatherworking = 7,
    Tailoring = 8
}

public static class Converter
{
    public static T? GetEnumValueOrDefault<T>(string value) where T : struct =>
        TryParseWithMemberName(value, out T result) ? (T?)result : default;

    public static string GetName(this Enum? value)
    {
        if (value is null) { return string.Empty; }

        var memberAttribute = value.GetType().GetField(value.ToString())?
            .GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

        return memberAttribute?.Value ?? value.ToString();
    }

    public static bool TryParseWithMemberName<T>(string? value, out T result) where T : struct
    {
        result = default;

        if (string.IsNullOrEmpty(value)) { return false; }

        foreach (var name in Enum.GetNames(typeof(T)))
        {
            if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                result = Enum.Parse<T>(name);
                return true;
            }

            var memberAttribute = typeof(T).GetField(name)?
                .GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

            if (memberAttribute?.Value?.Equals(value, StringComparison.OrdinalIgnoreCase) is not true)
            {
                continue;
            }

            result = Enum.Parse<T>(name);
            return true;
        }

        return false;
    }
}
