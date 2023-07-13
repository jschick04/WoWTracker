﻿using System.Security.Claims;
using System.Text.Json;

namespace Tracker.Shared.Helpers;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];

        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes) ??
            throw new Exception("Invalid JWT");

        ExtractRolesFromJwt(claims, keyValuePairs);

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

        return claims;
    }

    private static void ExtractRolesFromJwt(List<Claim> claims, IDictionary<string, object> keyValuePairs)
    {
        if (!keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles)) { return; }

        if (roles.ToString()!.Trim().StartsWith("["))
        {
            var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

            claims.AddRange(parsedRoles!.Select(role => new Claim(ClaimTypes.Role, role)));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
        }

        keyValuePairs.Remove(ClaimTypes.Role);
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2 :
                base64 += "==";
                break;
            case 3 :
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}
