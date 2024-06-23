using KSoftTask.Infrastructure.Interfaces;
using KSoftTask.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace KSoftTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAuthenticationService _authService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserAuthenticationService authService, ITokenService tokenService) => (_authService, _tokenService) = (authService, tokenService);

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegistrationModel registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error");
            }

            return Ok(await _authService.RegisterAsync(registration));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Authenticate(LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Error");
            }

            return Ok(await _authService.LoginAsync(login));
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshToken(TokenModel token) => Ok(await _tokenService.RefreshToken(token));

        [HttpPost("revoke/{username}")]
        public async Task<ActionResult> Revoke(string username) => Ok(await _tokenService.RevokeUser(username));

        [HttpPost("revokeAll")]
        public async Task<ActionResult> RevokeAll() => Ok(await _tokenService.RevokeAll());

        [HttpPost("logout")]
        public async Task Logout() => await _authService.LogoutAsync();
    }
}
