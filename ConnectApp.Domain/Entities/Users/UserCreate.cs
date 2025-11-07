using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    public partial class User
    {
        public static async Task<User> CreateAsync(
            UserParams @params


           
            )
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                CPF = cpf,
                AccessKey = accessKey,
                Password = password,
                RecordStatus = true,
                IsActive = true,
                Roles = new List<string> { "User" },
                Phones = new List<Phone>(),
                Addresses = new List<Address>(),
                Emails = new List<Email>()
            };
            return user;
        }
    }

    
}
