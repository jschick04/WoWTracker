using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Authorization;
using TrackerApi.Contracts.V1;
using TrackerApi.Contracts.V1.Requests;
using TrackerApi.Entities;
using TrackerApi.Managers;

namespace TrackerApi.Controllers.Identity {

    public class TokenController : BaseApiController {

        private readonly IIdentityManager _identity;

        public TokenController(IIdentityManager identity) => _identity = identity;

        [HttpPost(ApiRoutes.Identity.RefreshToken)]
        public async Task<IActionResult> RefreshToken() {
            var refreshToken = Request.Cookies["RefreshToken"];
            var response = await _identity.RefreshTokenAsync(refreshToken, GetIpAddress());

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [Authorize]
        [HttpPost(ApiRoutes.Identity.RevokeToken)]
        public async Task<IActionResult> RevokeToken([FromBody] TokenRequest request) {
            var token = request.Token ?? Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(token)) { return BadRequest(new { Error = "Token is required" }); }

            if (!Account.OwnsToken(token) && Account.Role != Role.Admin) {
                return Unauthorized();
            }

            await _identity.RevokeTokenAsync(token, GetIpAddress());

            return NoContent();
        }

        [HttpPost(ApiRoutes.Identity.ValidateToken)]
        public async Task<IActionResult> ValidateResetToken([FromBody] TokenRequest request) {
            await _identity.ValidateResetTokenAsync(request);

            return Ok();
        }

    }

}