using Microsoft.AspNetCore.Mvc;
using Tracker.Api.Contracts.Identity.Requests;
using Tracker.Api.Contracts.Routes;
using Tracker.Api.Managers;

namespace Tracker.Api.Controllers.Identity;

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

        //SetTokenCookie(response.RefreshToken);
        SetRefreshTokenHeader(response.RefreshToken!);

        return Ok(response);
    }

    [HttpPost(ApiRoutes.Identity.Register)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request) {
        await _user.RegisterAsync(request, Request.Headers["origin"]);

        return Ok("Account successfully registered, please check email for verification");
    }

}