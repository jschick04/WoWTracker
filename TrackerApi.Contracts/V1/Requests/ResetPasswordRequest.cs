using System.ComponentModel.DataAnnotations;
using TrackerApi.Contracts.Helpers;

namespace TrackerApi.Contracts.V1.Requests {

    public class ResetPasswordRequest {

        [Required]
        public string Token { get; set; }

        [Required]
        [Password]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }

}