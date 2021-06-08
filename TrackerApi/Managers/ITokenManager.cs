using TrackerApi.Entities;

namespace TrackerApi.Managers {

    public interface ITokenManager {

        string GenerateJwtToken(User user);

        string GenerateRandomTokenString();

        RefreshToken GenerateRefreshToken(string ipAddress);

        void RemoveOldRefreshTokens(User user);

        int? ValidateToken(string token);

    }

}