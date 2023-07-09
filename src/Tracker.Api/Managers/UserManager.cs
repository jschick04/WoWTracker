using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Data;
using Tracker.Api.Entities;
using Tracker.Api.Library.Helpers;

namespace Tracker.Api.Managers;

public class UserManager : IUserManager
{
    private readonly DataContext _data;
    private readonly IEmailManager _emailManager;
    private readonly ITokenManager _tokenManager;

    public UserManager(DataContext data, IEmailManager emailManager, ITokenManager tokenManager)
    {
        _data = data;
        _emailManager = emailManager;
        _tokenManager = tokenManager;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetUserAsync(id);

        _data.Users.Remove(user);
        await _data.SaveChangesAsync();
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request, string? origin)
    {
        var user = await _data.Users.SingleOrDefaultAsync(u => u.Username == request.Username);

        if (user is null) { throw new ApiException("An error has occurred"); }

        user.ResetToken = _tokenManager.GenerateEmailToken(user.Username);
        user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

        _data.Users.Update(user);
        await _data.SaveChangesAsync();

        _emailManager.SendForgotPassword(user, origin);
    }

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return (await _data.Users.ToListAsync()).Select(user => new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role.ToString(),
                Created = user.Created,
                Updated = user.Updated,
                IsVerified = user.IsVerified
            }
        ).ToList();
    }

    public async Task<UserResponse> GetByIdAsync(int id)
    {
        var user = await GetUserAsync(id);

        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Role = user.Role.ToString(),
            Created = user.Created,
            Updated = user.Updated,
            IsVerified = user.IsVerified
        };
    }

    public async Task RegisterAsync(RegistrationRequest request, string? origin)
    {
        if (await _data.Users.AnyAsync(u => u.Username == request.Username))
        {
            throw new ApiException("Username is already registered");
        }

        var isFirstAccount = await _data.Users.AnyAsync() == false;

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = isFirstAccount ? Role.Admin : Role.User,
            Created = DateTime.UtcNow,
            Verified = isFirstAccount ? DateTime.UtcNow : null,
            VerificationToken = isFirstAccount ? null : _tokenManager.GenerateEmailToken(request.Username)
        };

        await _data.Users.AddAsync(user);
        await _data.SaveChangesAsync();

        if (!isFirstAccount)
        {
            _emailManager.SendVerification(user, origin);
        }
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _data.Users.SingleOrDefaultAsync(
            t => t.ResetToken == request.Token && t.ResetTokenExpires > DateTime.UtcNow
        );

        if (user is null) { throw new ApiException("An error has occured"); }

        CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.ResetToken = null;
        user.ResetTokenExpires = null;

        _data.Users.Update(user);
        await _data.SaveChangesAsync();
    }

    public async Task<UserResponse> UpdateAsync(int id, UpdateRequest request)
    {
        var user = await GetUserAsync(id);

        if (user.Username != request.Username && _data.Users.Any(u => u.Username == request.Username))
        {
            throw new ApiException("Username is already registered");
        }

        if (request.Password is not null)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }

        user.FirstName = request.FirstName ?? user.FirstName;
        user.LastName = request.LastName ?? user.LastName;
        user.Username = request.Username ?? user.Username;

        user.Updated = DateTime.UtcNow;

        _data.Users.Update(user);
        await _data.SaveChangesAsync();

        return new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Role = user.Role.ToString(),
            Created = user.Created,
            Updated = user.Updated,
            IsVerified = user.IsVerified
        };
    }

    public async Task VerifyEmailAsync(string token)
    {
        var user = await _data.Users.SingleOrDefaultAsync(u => u.VerificationToken == token);

        if (user is null) { throw new ApiException("Verification failed"); }

        user.Verified = DateTime.UtcNow;
        user.VerificationToken = null;

        _data.Users.Update(user);
        await _data.SaveChangesAsync();
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Value cannot be empty or contain only whitespace", nameof(password));
        }

        using var hmac = new HMACSHA512();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private async Task<User> GetUserAsync(int id)
    {
        var user = await _data.Users.FindAsync(id);

        if (user is null) { throw new KeyNotFoundException("User not found"); }

        return user;
    }
}
