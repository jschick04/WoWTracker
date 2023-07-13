using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Tracker.Api.Entities;

[Owned]
public record RefreshToken
{
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;

    public DateTime Created { get; set; }

    public string CreatedByIp { get; set; } = null!;

    public DateTime? Revoked { get; set; }

    public string? RevokedByIp { get; set; }

    public string? ReplacedByToken { get; set; }

    public bool IsActive => Revoked == null && !IsExpired;
}
