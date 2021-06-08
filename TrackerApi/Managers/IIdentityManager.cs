using System.Threading.Tasks;
using TrackerApi.Contracts.V1.Requests;
using TrackerApi.Contracts.V1.Responses;

namespace TrackerApi.Managers {

    public interface IIdentityManager {

        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

        Task<AuthenticationResponse> RefreshTokenAsync(string token, string ipAddress);

        Task RevokeTokenAsync(string token, string ipAddress);

        Task ValidateResetTokenAsync(TokenRequest request);

    }

}