using ConnectApp.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Interfaces.Accounts
{

    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(Guid id);
        Task<Account> GetByIdAsync(AccountParams id);
        Task<List<Account>> GetAllAsync();
        Task<AccountResult> CreateAsync(AccountParams account);
        //Task<bool> UpdateAsync(Account account);
        Task<bool> UpdateAsync(AccountParams account);
        Task<bool> DeleteAsync(Guid id);
    }

}
