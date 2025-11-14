using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Application.DTOs.Users
{
    public class UserParams
    {
        public Guid? Id { get; set; } 
        public string Name { get; set; } = null!;
        public string? CPF { get; set; }
        public string AccessKey { get; set; } = null!;
        public string Password { get; set; } = null!;
        public IList<Phone>? Phones { get; set; }
        public IList<Address>? Addresses { get; set; }
        public IList<Email>? Emails { get; set; }
        public string? Avatar { get; set; }
        public string? Note { get; set; }
        public IList<string>? Roles { get; set; }
    }
}



