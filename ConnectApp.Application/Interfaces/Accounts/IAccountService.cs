using ConnectApp.Application.DTOs.Accounts;

namespace ConnectApp.Application.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<AccountResult> CreateAccountAsync(AccountParams @params);
        Task<AccountResult> GetAccountByIdAsync(Guid id);
        Task<List<AccountResult>> GetAllAccountsAsync();
        Task<AccountResult> UpdateAccountByIdAsync(AccountParams account);
        Task<AccountResult> DeleteAccountAsync(Guid id, Guid userId, string userName);
    }
}
