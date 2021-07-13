using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.Identity.Requests {

    public class ForgotPasswordRequest {

        [Required]
        [EmailAddress]
        public string Username { get; set; }

    }

}