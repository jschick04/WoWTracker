using System.Net.Http.Json;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Contracts.V1.Responses;
using Tracker.Library.Helpers;

namespace Tracker.Library.Managers;

public class ItemManager : IItemManager
{
    private readonly HttpClient _httpClient;

    public ItemManager(HttpClient httpClient) => _httpClient = httpClient;

    public Dictionary<string, List<ItemResponse>>? Items { get; private set; }

    public async Task<IResult> GetAllAsync()
    {
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.GetAsync(ApiRoutes.Item.GetAll);
        }
        catch (Exception ex)
        {
            return await Result.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<Dictionary<string, List<ItemResponse>>>();
            Items = data;
            return await Result.SuccessAsync();
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result.FailAsync(message?.Error);
    }

    public async Task<Result<List<NeededItemResponse>>> GetCraftableByProfession(string profession)
    {
        HttpResponseMessage response;

        try
        {
            response = await _httpClient.GetAsync(ApiRoutes.Item.GetCraftableByProfessionReplace(profession));
        }
        catch (Exception ex)
        {
            return await Result<List<NeededItemResponse>>.FailAsync(ex.Message);
        }

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<List<NeededItemResponse>>();
            return await Result<List<NeededItemResponse>>.SuccessAsync(data);
        }

        var message = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        return await Result<List<NeededItemResponse>>.FailAsync(message?.Error);
    }
}
