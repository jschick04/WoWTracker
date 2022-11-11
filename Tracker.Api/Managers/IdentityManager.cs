using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Data;
using Tracker.Api.Entities;
using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Managers;

public class IdentityManager : IIdentityManager
{
    private readonly DataContext _data;
    private readonly ITokenManager _tokenManager;

    public IdentityManager(DataContext data, ITokenManager tokenManager)
    {
        _data = data;
        _tokenManager = tokenManager;
    }

    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request, string? ipAddress)
    {
        var user = await _data.Users.SingleOrDefaultAsync(u => u.Username == request.Username);

        if (user is null) { throw new KeyNotFoundException("User not found"); }

        if (!user.IsVerified) { throw new ApiException("Email not confirmed"); }

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new ApiException("Invalid Credentials");
        }

        var token = _tokenManager.GenerateJwtToken(user);
        var refreshToken = _tokenManager.GenerateRefreshToken(ipAddress);
        user.RefreshTokens.Add(refreshToken);

        _tokenManager.RemoveOldRefreshTokens(user);

        _data.Update(user);
        await _data.SaveChangesAsync();

        return new AuthenticationResponse
        {
            Id = user.Id,
            Username = user.Username,
            Created = user.Created,
            Updated = user.Updated,
            IsVerified = user.IsVerified,
            Token = token,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<AuthenticationResponse> RefreshTokenAsync(string? token, string ipAddress)
    {
        (RefreshToken refreshToken, User user) = await GetRefreshTokenAsync(token);

        var newRefreshToken = _tokenManager.GenerateRefreshToken(ipAddress);
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        user.RefreshTokens.Add(newRefreshToken);

        _tokenManager.RemoveOldRefreshTokens(user);

        _data.Update(user);
        await _data.SaveChangesAsync();

        var jwtToken = _tokenManager.GenerateJwtToken(user);

        return new AuthenticationResponse
        {
            Id = user.Id,
            Username = user.Username,
            Created = user.Created,
            Updated = user.Updated,
            IsVerified = user.IsVerified,
            Token = jwtToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    public async Task RevokeTokenAsync(string token, string ipAddress)
    {
        (RefreshToken refreshToken, User user) = await GetRefreshTokenAsync(token);

        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;

        _data.Update(user);
        await _data.SaveChangesAsync();
    }

    public async Task ValidateResetTokenAsync(TokenRequest request)
    {
        var user = await _data.Users.SingleOrDefaultAsync(
            t => t.ResetToken == request.Token && t.ResetTokenExpires > DateTime.UtcNow
        );

        if (user is null) { throw new KeyNotFoundException("Invalid Token"); }
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        if (password is null) { throw new ArgumentNullException(nameof(password)); }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Value cannot be empty or contain only whitespace", nameof(password));
        }

        if (passwordHash.Length != 64)
        {
            throw new ArgumentException("Invalid length of password hash", nameof(passwordHash));
        }

        if (passwordSalt.Length != 128)
        {
            throw new ArgumentException("Invalid length of password salt", nameof(passwordSalt));
        }

        using var hmac = new HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != passwordHash[i]) { return false; }
        }

        return true;
    }

    private async Task<(RefreshToken, User)> GetRefreshTokenAsync(string? token)
    {
        var user = await _data.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

        if (user is null) { throw new ApiException("Invalid Token"); }

        var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

        if (!refreshToken.IsActive) { throw new ApiException("Invalid Token"); }

        return (refreshToken, user);
    }
}
