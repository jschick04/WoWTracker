namespace Tracker.Api.Contracts.Identity.Responses;

public sealed record UserResponse
{
    public int Id { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Username { get; init; }

    public string? Role { get; init; }

    public DateTime Created { get; init; }

    public DateTime? Updated { get; init; }

    public bool IsVerified { get; init; }
}
