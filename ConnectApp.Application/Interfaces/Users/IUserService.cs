using ConnectApp.Application.DTOs.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<UserResult> CreateAsync(UserParams userParams);
        //Task<UserResult> LoginAsync(string accessKey, string password);
        Task<List<UserResult?>> GetAllusersAsync();
        Task<UserResult?> GetUserByIdAsync(UserParams userParams);
        Task<UserResult?> UpdateUserByIdAsync(UserParams userParams);
        Task<Credentials?> GetCredentialByIdAsync(Guid userId);
    }
}
