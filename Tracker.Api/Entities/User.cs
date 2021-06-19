using System;
using System.Collections.Generic;

namespace Tracker.Api.Entities {

    public class User {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public Role Role { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Verified { get; set; }

        public string VerificationToken { get; set; }

        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;

        public string ResetToken { get; set; }

        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? PasswordReset { get; set; }

        public bool OwnsToken(string token) => RefreshTokens?.Find(t => t.Token == token) is not null;

    }

}