using System.Collections.Generic;
using System.Threading.Tasks;
using TrackerApi.Contracts.V1.Requests;
using TrackerApi.Contracts.V1.Responses;

namespace TrackerApi.Managers {

    public interface IUserManager {

        Task DeleteAsync(int id);

        Task ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

        Task<IEnumerable<UserResponse>> GetAllAsync();

        Task<UserResponse> GetByIdAsync(int id);

        Task RegisterAsync(RegistrationRequest request, string origin);

        Task ResetPasswordAsync(ResetPasswordRequest request);

        Task<UserResponse> UpdateAsync(int id, UpdateRequest request);

        Task VerifyEmailAsync(string token);

    }

}