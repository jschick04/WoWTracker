using System.Text.Json.Serialization;

namespace Tracker.Api.Contracts.Identity.Responses;

public sealed record AuthenticationResponse
{
    public int Id { get; init; }

    public string? Username { get; init; }

    public DateTime Created { get; init; }

    public DateTime? Updated { get; init; }

    public bool IsVerified { get; init; }

    public string? Token { get; init; }

    [JsonIgnore]
    public string? RefreshToken { get; init; }
}
