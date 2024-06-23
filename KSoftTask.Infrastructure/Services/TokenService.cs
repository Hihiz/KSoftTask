using KSoftTask.Infrastructure.Helpers;
using KSoftTask.Infrastructure.Identity;
using KSoftTask.Infrastructure.Interfaces;
using KSoftTask.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace KSoftTask.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(IConfiguration configuration, UserManager<ApplicationUser> userManager) => (_configuration, _userManager) = (configuration, userManager);

        public async Task<TokenModel> RefreshToken(TokenModel token)
        {
            if (token == null)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            string accessToken = token.AccessToken;
            string refreshToken = token.RefreshToken;

            var principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            string userName = principal.Identity.Name;

            ApplicationUser user = await _userManager.FindByNameAsync(userName);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            string newAccessToken = _configuration.GenerateAccessToken(principal.Claims.ToList());
            string newRefreshToken = _configuration.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<string> RevokeUser(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                return "Invalid username";
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return "OK";
        }

        public async Task<string> RevokeAll()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                user.RefreshToken = null;

                await _userManager.UpdateAsync(user);
            }

            return "OK";
        }
    }
}
