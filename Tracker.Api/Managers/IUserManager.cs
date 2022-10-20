using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;

namespace Tracker.Api.Managers;

public interface IUserManager
{
    Task DeleteAsync(int id);

    Task ForgotPasswordAsync(ForgotPasswordRequest request, string origin);

    Task<IEnumerable<UserResponse>> GetAllAsync();

    Task<UserResponse> GetByIdAsync(int id);

    Task RegisterAsync(RegistrationRequest request, string origin);

    Task ResetPasswordAsync(ResetPasswordRequest request);

    Task<UserResponse> UpdateAsync(int id, UpdateRequest request);

    Task VerifyEmailAsync(string token);
}
