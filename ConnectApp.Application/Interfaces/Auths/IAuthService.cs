using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Interfaces.Auths
{
    public interface IAuthService 
    {
        Task<Result<bool>> LoginExistsAsync(AuthCheck @params);        
        Task<Result<AuthResult>> SignInAsync(AuthParams @params);


        //Task<AuthResult> SignUpAsync(AuthParams @params);
        /* Task<bool> LoginExistsAsync(AuthCheck @params);
         Task<AuthResult> SignUpAsync(AuthCheck @params);
         Task<AuthResult> SignInAsync(AuthCheck @params);
        */
    }
}
