using System.Threading.Tasks;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers {

    public interface IUserManager {

        Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request);

        Task<Result<UserResponse>> GetAsync(int id);

        Task<IResult> RegisterAsync(RegistrationRequest request);

        Task<IResult> ResetPasswordAsync(ResetPasswordRequest request);

    }

}