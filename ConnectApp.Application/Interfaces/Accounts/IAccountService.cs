using ConnectApp.Application.DTOs.Accounts;

namespace ConnectApp.Application.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<AccountResult> CreateAccountAsync(AccountParams @params);
        Task<AccountResult> GetAccountByIdAsync(Guid id);
        Task<IList<AccountResult>> GetAllAccountsAsync();
        Task<AccountResult> UpdateAccountByIdAsync(AccountParams account);
        Task<AccountResult> DeleteAccountAsync(AccountParams account);
    }
}
