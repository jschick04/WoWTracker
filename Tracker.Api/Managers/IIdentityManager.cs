using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;

namespace Tracker.Api.Managers;

public interface IIdentityManager
{
    Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string? ipAddress);

    Task<AuthenticationResponse> RefreshTokenAsync(string token, string ipAddress);

    Task RevokeTokenAsync(string token, string ipAddress);

    Task ValidateResetTokenAsync(TokenRequest request);
}
