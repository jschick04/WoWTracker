using System.Net.Http.Json;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers;

public class UserManager : IUserManager {

    private readonly HttpClient _httpClient;

    public UserManager(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request) {
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Account.ForgotPassword, request);

        if (response.IsSuccessStatusCode) {
            return await Result.SuccessAsync(await response.Content.ReadAsStringAsync());
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

    public async Task<Result<UserResponse>> GetAsync(int id) {
        var response = await _httpClient.GetAsync(ApiRoutes.Account.GetByIdReplace(id));

        if (response.IsSuccessStatusCode) {
            return await Result<UserResponse>.SuccessAsync(
                await response.Content.ReadFromJsonAsync<UserResponse>()
            );
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result<UserResponse>.FailAsync(message?.Error);
    }

    public async Task<IResult> RegisterAsync(RegistrationRequest request) {
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Identity.Register, request);

        if (response.IsSuccessStatusCode) {
            return await Result.SuccessAsync(await response.Content.ReadAsStringAsync());
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

    public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request) {
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Account.ResetPassword, request);

        if (response.IsSuccessStatusCode) {
            return await Result.SuccessAsync(await response.Content.ReadAsStringAsync());
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

}