using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers {

    public class CharacterManager : ICharacterManager {

        private readonly HttpClient _httpClient;

        public CharacterManager(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IResult> CreateAsync(CreateCharacterRequest request) {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Character.Create, request);

            if (response.IsSuccessStatusCode) {
                return await Result.SuccessAsync();
            }

            var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return await Result.FailAsync(message?.Error);
        }

        public async Task<IResult> DeleteAsync(int id) {
            var response = await _httpClient.DeleteAsync(ApiRoutes.Character.DeleteReplace(id));

            if (response.IsSuccessStatusCode) {
                return await Result.SuccessAsync();
            }

            var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return await Result.FailAsync(message?.Error);
        }

        public async Task<Result<List<CharacterResponse>>> GetAllAsync() {
            var response = await _httpClient.GetAsync(ApiRoutes.Character.GetAll);

            if (response.IsSuccessStatusCode) {
                var data = await response.Content.ReadFromJsonAsync<List<CharacterResponse>>();
                return await Result<List<CharacterResponse>>.SuccessAsync(data);
            }

            var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return await Result<List<CharacterResponse>>.FailAsync(message?.Error);
        }

        public async Task<Result<CharacterResponse>> GetByIdAsync(int id) {
            var response = await _httpClient.GetAsync(ApiRoutes.Character.GetByIdReplace(id));

            if (response.IsSuccessStatusCode) {
                var data = await response.Content.ReadFromJsonAsync<CharacterResponse>();
                return await Result<CharacterResponse>.SuccessAsync(data);
            }

            var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return await Result<CharacterResponse>.FailAsync(message?.Error);
        }

        public async Task<IResult> UpdateAsync(int id, UpdateCharacterRequest request) {
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Character.UpdateReplace(id), request);

            if (response.IsSuccessStatusCode) {
                return await Result.SuccessAsync();
            }

            var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
            return await Result.FailAsync(message?.Error);
        }

    }

}