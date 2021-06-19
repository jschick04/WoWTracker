using Tracker.Api.Entities;

namespace Tracker.Api.Managers {

    public interface ITokenManager {

        string GenerateJwtToken(User user);

        string GenerateRandomTokenString();

        RefreshToken GenerateRefreshToken(string ipAddress);

        void RemoveOldRefreshTokens(User user);

        int? ValidateToken(string token);

    }

}