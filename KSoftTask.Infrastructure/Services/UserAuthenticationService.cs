using KSoftTask.Infrastructure.Data;
using KSoftTask.Infrastructure.Helpers;
using KSoftTask.Infrastructure.Identity;
using KSoftTask.Infrastructure.Interfaces;
using KSoftTask.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Security.Cryptography;

namespace KSoftTask.Infrastructure.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, SignInManager<ApplicationUser> signInManager, IConfiguration configuration) =>
            (_userManager, _db, _signInManager, _configuration) = (userManager, db, signInManager, configuration);

        public async Task<RegistrationModel> RegisterAsync(RegistrationModel model)
        {
            ApplicationUser userExist = await _userManager.FindByEmailAsync(model.Email);

            if (userExist != null)
            {
                throw new Exception("Пользователь уже существует");
            }

            ApplicationUser user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Не удалось создать пользователя");
            }

            ApplicationUser findUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (findUser == null)
            {
                throw new Exception($"Пользователь {model.FirstName} {model.LastName} не найден");
            }

            if (await _db.Roles.FirstOrDefaultAsync(r => r.Name == "Member") == null)
            {
                await _db.Roles.AddAsync(new IdentityRole<long>
                {
                    Name = "Member",
                    NormalizedName = "MEMBER"
                });

                await _db.SaveChangesAsync();
            }

            await _userManager.AddToRoleAsync(findUser, "Member");

            return model;
        }

        public async Task<TokenModel> LoginAsync(LoginModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new Exception("Неверное имя пользователя");
            }
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new Exception("Неверный пароль");
            }

            List<long> roleId = await _db.UserRoles.Where(u => u.UserId == user.Id).Select(u => u.RoleId).ToListAsync();
            List<IdentityRole<long>> roles = await _db.Roles.Where(r => roleId.Contains(r.Id)).ToListAsync();

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

            if (!signInResult.Succeeded)
            {
                throw new Exception("BadRequest");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
            };

            foreach (var userRole in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.Name!));
            }

            string accessToken = _configuration.GenerateAccessToken(authClaims);

            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

            await _db.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken
            };
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    }
}
