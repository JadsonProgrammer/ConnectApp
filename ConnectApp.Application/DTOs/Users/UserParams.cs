using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Application.DTOs.Users
{
    public class UserParams
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public List<string> Roles { get; set; } = ["User"];
        public List<Phone> Phones { get; set; } = [];
        public List<Address> Addresses { get; set; } = [];
        public List<Email> Emails { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int IsActive { get; set; }
    }
}
