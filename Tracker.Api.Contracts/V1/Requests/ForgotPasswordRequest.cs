using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.V1.Requests {

    public class ForgotPasswordRequest {

        [Required]
        [EmailAddress]
        public string Username { get; set; }

    }

}