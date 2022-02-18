using Tracker.Api.Entities;

namespace Tracker.Api.Managers;

public interface ITokenManager {

    string GenerateEmailToken(string username);

    string GenerateJwtToken(User user);

    RefreshToken GenerateRefreshToken(string? ipAddress);

    bool GetUserId(string token, out int id);

    bool IsAdminClaim(string token);

    void RemoveOldRefreshTokens(User user);

}