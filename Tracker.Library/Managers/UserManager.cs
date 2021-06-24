using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers {

    public class UserManager : IUserManager {

        private readonly HttpClient _httpClient;

        public UserManager(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request) {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Account.ForgotPassword, request);

            if (!response.IsSuccessStatusCode) {
                return await Result.FailAsync(await response.Content.ReadAsStringAsync());
            }

            return await Result.SuccessAsync(await response.Content.ReadAsStringAsync());
        }

        public async Task<Result<UserResponse>> GetAsync(int id) {
            var response = await _httpClient.GetAsync(ApiRoutes.Account.GetByIdReplace(id));

            if (!response.IsSuccessStatusCode) {
                return await Result<UserResponse>.FailAsync(await response.Content.ReadAsStringAsync());
            }

            return await Result<UserResponse>.SuccessAsync(await response.Content.ReadFromJsonAsync<UserResponse>());
        }

        public async Task<IResult> RegisterAsync(RegistrationRequest request) {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Identity.Register, request);

            if (!response.IsSuccessStatusCode) {
                return await Result.FailAsync(await response.Content.ReadAsStringAsync());
            }

            return await Result.SuccessAsync(await response.Content.ReadAsStringAsync());
        }

    }

}