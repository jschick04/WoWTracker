using System.ComponentModel.DataAnnotations;
using TrackerApi.Contracts.Helpers;

namespace TrackerApi.Contracts.V1.Requests {

    public class AuthenticationRequest {

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [Password]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

}