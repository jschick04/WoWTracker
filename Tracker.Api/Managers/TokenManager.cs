using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Tracker.Api.Entities;
using Tracker.Api.Settings;

namespace Tracker.Api.Managers {

    public class TokenManager : ITokenManager {

        private static JwtSettings _jwtSettings;
        private static TokenValidationParameters _validationParameters;

        public TokenManager(JwtSettings jwtSettings, TokenValidationParameters validationParameters) {
            _jwtSettings = jwtSettings;
            _validationParameters = validationParameters;
        }

        public string GenerateEmailToken(string username) {
            if (string.IsNullOrWhiteSpace(username)) {
                throw new ArgumentException("Value cannot be empty or contain only whitespace", nameof(username));
            }

            var token = GenerateRandomTokenString() + username;

            using var hmac = new HMACSHA512();

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));

            return WebEncoders.Base64UrlEncode(hash);
        }

        public string GenerateJwtToken(User user) {
            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.GivenName, user.FirstName),
                new(ClaimTypes.Surname, user.LastName),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var token = GenerateEncryptedToken(GetSigningCredentials(), claims);

            return token;
        }

        public RefreshToken GenerateRefreshToken(string ipAddress) =>
            new() {
                Token = GenerateRandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetimeDays),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

        public bool GetUserId(string token, out int id) {
            try {
                var principal = GetPrincipal(token);

                id = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));

                return true;
            } catch {
                id = 0;

                return false;
            }
        }

        public bool IsAdminClaim(string token) {
            try {
                var principal = GetPrincipal(token);

                return principal.IsInRole(Role.Admin.ToString());
            } catch {
                return false;
            }
        }

        public void RemoveOldRefreshTokens(User user) {
            user.RefreshTokens.RemoveAll(
                token => !token.IsActive && token.Created.AddDays(_jwtSettings.RefreshTokenHistoryDays) <= DateTime.UtcNow
            );
        }

        private static string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims) {
            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
                signingCredentials:signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        private static string GenerateRandomTokenString() {
            var randomBytes = new byte[32];

            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        private static ClaimsPrincipal GetPrincipal(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, _validationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256Signature,
                    StringComparison.InvariantCultureIgnoreCase
                )) {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }

        private static SigningCredentials GetSigningCredentials() {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }

    }

}