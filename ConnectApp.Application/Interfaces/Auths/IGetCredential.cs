using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Application.Interfaces.Auths
{
    public interface IGetCredential
    {
        Task<Guid> GetUserId();
        Task<string> GetUserName();
        Task<Guid> GetAccountId();
        Task<string> GetAccountName();


    }
}
