using KSoftTask.Infrastructure.Models;

namespace KSoftTask.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        Task<TokenModel> RefreshToken(TokenModel token);
        Task<string> RevokeUser(string username);
        Task<string> RevokeAll();
    }
}
