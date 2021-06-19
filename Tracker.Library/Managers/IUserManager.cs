using System.Threading.Tasks;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers {

    public interface IUserManager {

        Task<Result<UserResponse>> GetAsync(string id);

        Task<IResult> RegisterAsync(RegistrationRequest request);

    }

}