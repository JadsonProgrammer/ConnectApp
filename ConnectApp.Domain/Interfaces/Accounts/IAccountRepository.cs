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
        //---------------READ-----------------------
        Task<Account?> GetByIdAsync(Guid id);
        
        Task<List<Account>> GetAllAsync();


        //---------------CREATE----------------------
        Task<Account> CreateAsync(Account account);


        //---------------UPDATE-------------------
        Task<bool> UpdateAsync(Account account);


        //---------------DELETE-------------------
        Task<bool> DeleteAsync(Guid id);
    }

}
