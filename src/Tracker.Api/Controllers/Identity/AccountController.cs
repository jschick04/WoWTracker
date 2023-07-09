using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Authorization;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Identity.Responses;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Entities;
using Tracker.Api.Managers;

namespace Tracker.Api.Controllers.Identity;

public class AccountController : BaseApiController
{
    private readonly IUserManager _user;

    public AccountController(IUserManager user) => _user = user;

    [Authorize]
    [HttpDelete(ApiRoutes.Account.Delete)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id != Account?.Id && Account?.Role != Role.Admin)
        {
            return Unauthorized();
        }

        await _user.DeleteAsync(id);

        return NoContent();
    }

    [HttpPost(ApiRoutes.Account.ForgotPassword)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _user.ForgotPasswordAsync(request, Request.Headers["origin"]);

        return Ok("Password reset email has been sent");
    }

    [Authorize(Role.Admin)]
    [HttpGet(ApiRoutes.Account.GetAll)]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll() => Ok(await _user.GetAllAsync());

    [Authorize]
    [HttpGet(ApiRoutes.Account.GetById)]
    public async Task<ActionResult<UserResponse>> GetById([FromRoute] int id)
    {
        if (id != Account?.Id && Account?.Role != Role.Admin)
        {
            return Unauthorized();
        }

        return Ok(await _user.GetByIdAsync(id));
    }

    [HttpPost(ApiRoutes.Account.ResetPassword)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _user.ResetPasswordAsync(request);

        return Ok("Password successfully reset, you may now log in");
    }

    [Authorize]
    [HttpPut(ApiRoutes.Account.Update)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequest request)
    {
        if (id != Account?.Id && Account?.Role != Role.Admin)
        {
            return Unauthorized();
        }

        return Ok(await _user.UpdateAsync(id, request));
    }

    [HttpPost(ApiRoutes.Account.VerifyEmail)]
    public async Task<IActionResult> VerifyEmail([FromQuery] TokenRequest request)
    {
        await _user.VerifyEmailAsync(request.Token);

        return Ok("Account confirmed, you may now log in");
    }
}
