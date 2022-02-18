using System.Security.Claims;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Library.Helpers;

namespace Tracker.Client.Library.Managers.Authentication;

public interface IAuthenticationManager {

    Task<ClaimsPrincipal> GetCurrentUserClaims();

    Task<IResult> Login(AuthenticationRequest request);

    Task<IResult> Logout();

    Task<string?> TryRefreshToken();

}