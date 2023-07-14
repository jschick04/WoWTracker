using System.Security.Claims;
using FluentResults;
using Tracker.Api.Contracts.Identity.Requests;

namespace Tracker.UI.Library.Managers.Authentication;

public interface IAuthenticationManager
{
    Task<ClaimsPrincipal> GetCurrentUserClaims();

    Task<Result> Login(AuthenticationRequest request);

    Task<Result> Logout();

    Task<string?> TryRefreshToken();
}
