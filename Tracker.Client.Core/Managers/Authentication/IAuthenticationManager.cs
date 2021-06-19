using System.Security.Claims;
using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Library.Helpers;

namespace Tracker.Client.Core.Managers.Authentication {

    public interface IAuthenticationManager {

        Task<ClaimsPrincipal> GetCurrentUserClaims();

        Task<IResult> Login(AuthenticationRequest request);

        Task<IResult> Logout();

        Task<string> RefreshToken();

        Task<string> TryRefreshToken();

    }

}