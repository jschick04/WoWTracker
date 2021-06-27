using System;
using System.Text.Json.Serialization;

namespace Tracker.Api.Contracts.V1.Responses {

    public class AuthenticationResponse {

        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public bool IsVerified { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

    }

}