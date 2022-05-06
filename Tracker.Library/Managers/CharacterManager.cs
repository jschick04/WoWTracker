using System.Net.Http.Json;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers;

public class CharacterManager : ICharacterManager {

    private readonly HttpClient _httpClient;

    public CharacterManager(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<IResult> CreateAsync(CreateCharacterRequest request) {
        HttpResponseMessage response;

        try {
            response = await _httpClient.PostAsJsonAsync(ApiRoutes.Character.Create, request);
        } catch (Exception ex) {
            return await Result.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode) {
            return await Result.SuccessAsync();
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

    public async Task<IResult> DeleteAsync(int id) {
        HttpResponseMessage response;

        try {
            response = await _httpClient.DeleteAsync(ApiRoutes.Character.DeleteReplace(id));
        } catch (Exception ex) {
            return await Result.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode) {
            return await Result.SuccessAsync();
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

    public async Task<Result<List<CharacterResponse>>> GetAllAsync() {
        HttpResponseMessage response;

        try {
            response = await _httpClient.GetAsync(ApiRoutes.Character.GetAll);
        } catch (Exception ex) {
            return await Result<List<CharacterResponse>>.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode) {
            var data = await response.Content.ReadFromJsonAsync<List<CharacterResponse>>();
            return await Result<List<CharacterResponse>>.SuccessAsync(data);
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result<List<CharacterResponse>>.FailAsync(message?.Error);
    }

    public async Task<Result<CharacterResponse>> GetByIdAsync(int id) {
        HttpResponseMessage response;

        try {
            response = await _httpClient.GetAsync(ApiRoutes.Character.GetByIdReplace(id));
        } catch (Exception ex) {
            return await Result<CharacterResponse>.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode) {
            var data = await response.Content.ReadFromJsonAsync<CharacterResponse>();
            return await Result<CharacterResponse>.SuccessAsync(data);
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result<CharacterResponse>.FailAsync(message?.Error);
    }

    public async Task<IResult> UpdateAsync(int id, UpdateCharacterRequest request) {
        HttpResponseMessage response;

        try {
            response = await _httpClient.PutAsJsonAsync(ApiRoutes.Character.UpdateReplace(id), request);
        } catch (Exception ex) {
            return await Result.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode) {
            return await Result.SuccessAsync();
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

}