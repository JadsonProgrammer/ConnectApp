using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Domain.Interfaces.Auths.Tokens
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string storedHash);
    }
}
