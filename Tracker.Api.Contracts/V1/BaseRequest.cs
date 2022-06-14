namespace Tracker.Api.Contracts.V1;

public abstract class BaseRequest {

    protected static string? ReplaceEmptyWithNull(string? value) => string.IsNullOrWhiteSpace(value) ? null : value;

}