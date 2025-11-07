using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Entities.Auths;

namespace ConnectApp.Domain.Interfaces.Auths
{

    public interface IAuthRepository
    {
        Task<bool> GetByUsernameAsync(string login);        
        Task<User?> GetByUserAsync(string accessKey);
    }
}