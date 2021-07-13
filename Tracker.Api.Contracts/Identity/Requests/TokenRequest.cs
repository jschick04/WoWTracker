using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.Identity.Requests {

    public class TokenRequest {

        [Required]
        public string Token { get; set; }

    }

}