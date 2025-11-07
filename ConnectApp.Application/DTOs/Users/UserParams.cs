using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Application.DTOs.Users
{
    public class UserParams
    {

        public Guid? Id { get; set; } // somente no update

        public string Name { get; set; } = null!;
        public string? CPF { get; set; }
        public string AccessKey { get; set; } = null!;
        public string Password { get; set; } = null!;

        public IList<Phone>? Phones { get; set; }
        public IList<Address>? Addresses { get; set; }
        public IList<Email>? Emails { get; set; }

        public string? Avatar { get; set; }
        public string? Note { get; set; }

        // Opcional: roles se usuário pode alterar perfil dele (senão remover)
        public IList<string>? Roles { get; set; }
    }
}



