using KSoftTask.Infrastructure.Models;

namespace KSoftTask.Infrastructure.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<TokenModel> LoginAsync(LoginModel model);
        Task<RegistrationModel> RegisterAsync(RegistrationModel model);
        Task LogoutAsync();
    }
}
