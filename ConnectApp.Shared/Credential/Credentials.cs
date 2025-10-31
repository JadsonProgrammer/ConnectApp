using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Shared.Credential
{
    public class Credentials
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public Guid? AccountId { get; set; }
        public string? AccountName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
