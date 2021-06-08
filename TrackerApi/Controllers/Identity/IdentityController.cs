using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackerApi.Authorization;
using TrackerApi.Contracts.V1;
using TrackerApi.Contracts.V1.Requests;
using TrackerApi.Contracts.V1.Responses;
using TrackerApi.Entities;
using TrackerApi.Managers;

namespace TrackerApi.Controllers.Identity {

    public class IdentityController : BaseApiController {

        private readonly IIdentityManager _identity;
        private readonly IUserManager _user;

        public IdentityController(IIdentityManager identity, IUserManager user) {
            _identity = identity;
            _user = user;
        }

        [HttpPost(ApiRoutes.Identity.Authenticate)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request) {
            var response = await _identity.AuthenticateAsync(request, GetIpAddress());

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete(ApiRoutes.Identity.Delete)]
        public async Task<IActionResult> Delete(int id) {
            if (id != Account.Id && Account.Role != Role.Admin) {
                return Unauthorized();
            }

            await _user.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost(ApiRoutes.Identity.ForgotPassword)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request) {
            await _user.ForgotPasswordAsync(request, Request.Headers["origin"]);

            return Ok("Password reset email has been sent");
        }

        [Authorize(Role.Admin)]
        [HttpGet(ApiRoutes.Identity.GetAll)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll() => Ok(await _user.GetAllAsync());

        [Authorize]
        [HttpGet(ApiRoutes.Identity.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            if (id != Account.Id && Account.Role != Role.Admin) {
                return Unauthorized();
            }

            return Ok(await _user.GetByIdAsync(id));
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request) {
            await _user.RegisterAsync(request, Request.Headers["origin"]);

            return Ok("Account successfuly registered, please check email for verification");
        }

        [HttpPost(ApiRoutes.Identity.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request) {
            await _user.ResetPasswordAsync(request);

            return Ok("Password successfully reset, you may now log in");
        }

        [Authorize]
        [HttpPut(ApiRoutes.Identity.Update)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequest request) {
            if (id != Account.Id && Account.Role != Role.Admin) {
                return Unauthorized();
            }

            return Ok(await _user.UpdateAsync(id, request));
        }

        [HttpPost(ApiRoutes.Identity.VerifyEmail)]
        public async Task<IActionResult> VerifyEmail([FromBody] TokenRequest request) {
            await _user.VerifyEmailAsync(request.Token);

            return Ok("Account confirmed, you may now log in");
        }

    }

}