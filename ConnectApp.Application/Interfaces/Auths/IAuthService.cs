using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Interfaces.Auths
{
    public interface IAuthService : IResultService
    {
        Task<bool> LoginExistsAsync(AuthParams @params);
        Task<AuthResult> SignUpAsync(AuthParams @params);
        Task<AuthResult> SignInAsync(AuthParams @params);

    }
}
