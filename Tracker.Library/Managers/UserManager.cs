using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Tracker.Api.Contracts.V1;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers {

    public class UserManager : IUserManager {

        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public UserManager(IConfiguration config, HttpClient httpClient) {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<Result<UserResponse>> GetAsync(string id) {
            if (string.IsNullOrWhiteSpace(id)) {
                return await Result<UserResponse>.FailAsync("Invalid ID");
            }

            var api = _config["Api"] + ApiRoutes.Account.GetById.Replace("{id}", id);

            var response = await _httpClient.GetAsync(api);

            if (!response.IsSuccessStatusCode) {
                return await Result<UserResponse>.FailAsync(await response.Content.ReadAsStringAsync());
            }

            return await Result<UserResponse>.SuccessAsync(await response.Content.ReadFromJsonAsync<UserResponse>());
        }

        public async Task<IResult> RegisterAsync(RegistrationRequest request) {
            var api = _config["Api"] + ApiRoutes.Identity.Register;

            var response = await _httpClient.PostAsJsonAsync(api, request);

            if (!response.IsSuccessStatusCode) {
                return await Result.FailAsync(await response.Content.ReadAsStringAsync());
            }

            return await Result.SuccessAsync(await response.Content.ReadAsStringAsync());
        }

    }

}