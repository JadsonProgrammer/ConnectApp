using ConnectApp.Application.DTOs.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Interfaces.Users
{
    public interface IUserService
    {

        //-----------------------CREATE USER-----------------------//
        Task<UserResult> CreatesUserAsync(UserParams userParams);

       

        //-----------------------GET USERS-----------------------//
        Task<IList<UserResult?>> GetAllusersAsync();
        Task<IList<UserResult?>> GetUserByIdAsync(Guid id);

        //-----------------------UPDATE USER-----------------------//
        Task<UserResult> UpdateUserByIdAsync(UserParams userParams);


        //-----------------------DELETE USER-----------------------//

        Task<Result<bool>> DeleteUserByIdAsync(Guid userId);

        //-----------------------GET CREDENTIALS-----------------------//
        Task<Credentials?> GetCredentialByIdAsync(Guid userId);
    }
}
