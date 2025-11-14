using ConnectApp.Application.DTOs.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Interfaces.Users
{
    public interface IUserService
    {


        Task<Result<UserResult>> CreatesUserAsync(UserParams userParams);
        Task<Result<IList<UserResult>>> GetAllusersAsync();
        Task<Result<UserResult>> GetUserByIdAsync(Guid id);
        Task<Result<UserResult>> UpdateUserByIdAsync(UserParams userParams, Guid id);
        Task<Result<bool>> DeleteUserByIdAsync(Guid id);






























        ////-----------------------CREATE USER-----------------------//
        //Task<UserResult> CreatesUserAsync(UserParams userParams);



        ////-----------------------GET USERS-----------------------//
        //Task<IList<UserResult?>> GetAllusersAsync();
        //Task<IList<UserResult?>> GetUserByIdAsync(Guid id);

        ////-----------------------UPDATE USER-----------------------//
        ////Task<UserResult> UpdateUserByIdAsync(UserParams userParams);
        //Task<UserResult> UpdateUserByIdAsync(UserParams userParams, Guid? id);


        ////-----------------------DELETE USER-----------------------//

        //Task<Result<bool>> DeleteUserByIdAsync(Guid userId);

        ////-----------------------GET CREDENTIALS-----------------------//
        //Task<Credentials?> GetCredentialByIdAsync(Guid userId);
    }
}
