using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.V1.Requests {

    public class TokenRequest {

        [Required]
        public string Token { get; set; }

    }

}