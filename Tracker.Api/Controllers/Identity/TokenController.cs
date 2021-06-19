using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.V1;
using Tracker.Api.Contracts.V1.Requests;
using Tracker.Api.Entities;
using Tracker.Api.Managers;

namespace Tracker.Api.Controllers.Identity {

    public class TokenController : BaseApiController {

        private readonly IIdentityManager _identity;

        public TokenController(IIdentityManager identity) => _identity = identity;

        [HttpPost(ApiRoutes.Identity.RefreshToken)]
        public async Task<IActionResult> RefreshToken() {
            Request.Headers.TryGetValue("RefreshToken", out var refreshToken);

            var response = await _identity.RefreshTokenAsync(refreshToken, GetIpAddress());

            SetRefreshTokenHeader(response.RefreshToken);

            return Ok(response);
        }

        [Authorize]
        [HttpPost(ApiRoutes.Identity.RevokeToken)]
        public async Task<IActionResult> RevokeToken([FromBody] TokenRequest request) {
            if (string.IsNullOrEmpty(request.Token)) { return BadRequest(new { Error = "Token is required" }); }

            if (!Account.OwnsToken(request.Token) && Account.Role != Role.Admin) {
                return Unauthorized();
            }

            await _identity.RevokeTokenAsync(request.Token, GetIpAddress());

            return NoContent();
        }

        [HttpPost(ApiRoutes.Identity.ValidateToken)]
        public async Task<IActionResult> ValidateResetToken([FromBody] TokenRequest request) {
            await _identity.ValidateResetTokenAsync(request);

            return Ok();
        }

    }

}