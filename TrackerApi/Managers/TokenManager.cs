using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrackerApi.Entities;
using TrackerApi.Settings;

namespace TrackerApi.Managers {

    public class TokenManager : ITokenManager {

        private static JwtSettings _jwtSettings;
        private static TokenValidationParameters _validationParameters;

        public TokenManager(JwtSettings jwtSettings, TokenValidationParameters validationParameters) {
            _jwtSettings = jwtSettings;
            _validationParameters = validationParameters;
        }

        public string GenerateJwtToken(User user) {
            var claims = new List<Claim> {
                new(ClaimTypes.Name, user.Id.ToString())
            };

            var token = GenerateEncryptedToken(GetSigningCredentials(), claims);

            return token;
        }

        public string GenerateRandomTokenString() {
            var randomBytes = new byte[32];

            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress) =>
            new() {
                Token = GenerateRandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };

        public void RemoveOldRefreshTokens(User user) {
            user.RefreshTokens.RemoveAll(
                token => !token.IsActive && token.Created.Add(_jwtSettings.RefreshTokenLifetime) <= DateTime.UtcNow
            );
        }

        public int? ValidateToken(string token) {
            try {
                var principal = GetPrincipalFromToken(token);

                var userId = int.Parse(principal.FindFirstValue(ClaimTypes.Name));

                return userId;
            } catch {
                return null;
            }
        }

        private static string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims) {
            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                signingCredentials:signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        private static ClaimsPrincipal GetPrincipalFromToken(string token) {
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