using System.ComponentModel.DataAnnotations;

namespace TrackerApi.Contracts.V1.Requests {

    public class TokenRequest {

        [Required]
        public string Token { get; set; }

    }

}