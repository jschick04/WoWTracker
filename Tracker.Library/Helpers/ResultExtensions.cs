using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tracker.Library.Helpers;

public static class ResultExtensions {

    public static bool GetDataIfSuccess<T>(this Result<T> result, ref T data) {
        if (result.Succeeded is false) { return false; }

        if (result.Data is null) { return false; }

        data = result.Data;

        return true;
    }

    internal static async Task<IResult<T>> ToResult<T>(this HttpResponseMessage response) {
        var responseAsString = await response.Content.ReadAsStringAsync();

        var responseObject = JsonSerializer.Deserialize<Result<T>>(
            responseAsString,
            new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            }
        );

        return responseObject!;
    }

    internal static async Task<IResult> ToResult(this HttpResponseMessage response) {
        var responseAsString = await response.Content.ReadAsStringAsync();

        var responseObject = JsonSerializer.Deserialize<Result>(
            responseAsString,
            new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            }
        );

        return responseObject!;
    }

}