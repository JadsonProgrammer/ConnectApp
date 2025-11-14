using ConnectApp.Application.DTOs.Accounts;
using ConnectApp.Application.DTOs.Users;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<Result<AccountResult>> CreatesAccountAsync(AccountParams @params);
        Task<Result<IList<AccountResult>>> GetAllAccountsAsync();
        Task<Result<AccountResult>> GetAccountByIdAsync(Guid id);
        Task<Result<AccountResult>> UpdateAccountByIdAsync(AccountParams account, Guid id);

        Task<Result<AccountResult>> DeleteAccountAsync(Guid id);
    }
}
