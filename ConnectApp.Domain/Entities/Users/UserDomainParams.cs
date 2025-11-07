using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    internal class UserDomainParams
    {


        public Guid? Id { get; set; } // para update
        public string Name { get; set; } = string.Empty;
        public string? CPF { get; set; }
        public string AccessKey { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public IList<Phone>? Phones { get; set; }
        public IList<Address>? Addresses { get; set; }
        public IList<Email>? Emails { get; set; }

        public IList<string>? Roles { get; set; }
        public string? Avatar { get; set; }
        public string? Note { get; set; }
    }
}

