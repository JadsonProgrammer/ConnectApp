using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Application.Interfaces.Auths
{
    public interface IGetCredential
    {


        Task<Guid> GetUserIdAsync();
        Task<string> GetUserNameAsync();
        Task<Guid> GetAccountIdAsync();
        Task<string> GetAccountNameAsync();
        Task<User?> GetUserAsync();

















        /*
        Task<Guid> GetUserId();
        Task<string> GetUserName();
        Task<Guid> GetAccountId();
        Task<string> GetAccountName();

        */
    }
}
