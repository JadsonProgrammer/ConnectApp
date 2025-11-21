using ConnectApp.Shared.Helpers;

namespace ConnectApp.Domain.Entities.Users
{
    public partial class User
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? CPF { get; set; }
        public string AccessKey { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public IList<string> Roles { get; set; } = [];
        public IList<Phone> Phones { get; set; } = [];
        public IList<Address> Addresses { get; set; } = [];
        public IList<Email> Emails { get; set; } = [];

        public int? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public int? ProfileCode { get; set; }
        public string? ProfileName { get; set; }
        public int? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public DateTime? LastAccess { get; set; }
        public int? AccessCount { get; set; }
        public string? Avatar { get; set; }
        public bool RecordStatus { get; set; }
        public string? Note { get; set; }

        // Audit
        public Guid? BrokerId { get; set; } = Guid.Empty;
        public Guid? AccountId { get; set; } = Guid.Empty;
        public string? AccountName { get; set; } = string.Empty;
        public DateTime? CreationDate { get; set; }
        public Guid? CreationUserId { get; set; } = Guid.Empty;
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid? ChangeUserId { get; set; } = Guid.Empty;
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid? ExclusionUserId { get; set; } = Guid.Empty;
        public string? ExclusionUserName { get; set; }

        public User() { }

    }
}




/*
    public static User Create(
            string name,
            string cpf,
            string accessKey,
            string password,
            IList<Phone>? phones,
            IList<Address>? addresses,
            IList<Email>? emails,
            IList<string>? roles,
            Guid creationUserId,
            string creationUserName,
            Guid accountId,
            string accountName)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(accessKey)) throw new Exception("AccessKey é obrigatória.");
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("Password é obrigatória.");
            if (string.IsNullOrWhiteSpace(cpf)) throw new Exception("CPF é obrigatório");
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                CPF = cpf?.Trim(),
                AccessKey = accessKey,
                Password = password,
                Phones = phones ?? [],
                Addresses = addresses ?? [],
                Emails = emails ?? [],
                //Roles = roles != null && roles.Any() ? roles : new List<string> { "User" },
                AccountId = accountId,
                AccountName = accountName,
                
                RecordStatus = true,
                CreationDate = DateTime.UtcNow,
                CreationUserId = creationUserId,
                CreationUserName = creationUserName
            };

            user.ValidateForCreation();
            return user;
        }

        // Update behavior
        public void Update(
            string name,
            string? cpf,
            IList<Phone>? phones,
            IList<Address>? addresses,
            IList<Email>? emails,
            string? avatar,
            string? note,
            Guid changeUserId,
            string changeUserName)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório.");
            Name = name.Trim();
            CPF = cpf?.Trim();
            //Phones = phones ?? Phones;
            //Addresses = addresses ?? Addresses;
            //Emails = emails ?? Emails;
            Avatar = avatar;
            Note = note;
            ChangeDate = DateTime.UtcNow;
            ChangeUserId = changeUserId;
            ChangeUserName = changeUserName;

            ValidateForUpdate();
        }

        public void Deactivate(Guid exclusionUserId, string exclusionUserName)
        {
            RecordStatus = false;
            
            ExclusionDate = DateTime.UtcNow;
            ExclusionUserId = exclusionUserId;
            ExclusionUserName = exclusionUserName;
            ValidateForDeactivation();
        }

        // MÉTODOS DE VALIDAÇÃO (tudo na mesma classe partial)
        private void ValidateForCreation()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(AccessKey))
                errors.Add("AccessKey é obrigatória.");

            if (string.IsNullOrWhiteSpace(Password))
                errors.Add("Password é obrigatória.");

            if (!string.IsNullOrWhiteSpace(CPF) && !Validation.CPFValido(CPF))
                errors.Add("CPF inválido.");

            if (AccessKey.Length < 3)
                errors.Add("AccessKey deve ter pelo menos 3 caracteres.");

            if (Password.Length < 6)
                errors.Add("Password deve ter pelo menos 6 caracteres.");

            if (AccountId == Guid.Empty || AccountId == null)
                errors.Add("AccountId é obrigatório.");

            if (string.IsNullOrWhiteSpace(AccountName))
                errors.Add("AccountName é obrigatório.");

            if (CreationUserId == Guid.Empty || CreationUserId == null)
                errors.Add("CreationUserId é obrigatório.");

            if (string.IsNullOrWhiteSpace(CreationUserName))
                errors.Add("CreationUserName é obrigatório.");

            if (CreationDate == null)
                errors.Add("CreationDate é obrigatório.");

            if (errors.Count != 0)
                throw new ArgumentException($"Erros de validação na criação: {string.Join(" ", errors)}");
        }

        private void ValidateForUpdate()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("Nome é obrigatório.");

            if (!string.IsNullOrWhiteSpace(CPF) && !Validation.CPFValido(CPF))
                errors.Add("CPF inválido.");

            if(DateTime.Now < ChangeDate)
                errors.Add("ChangeDate não pode ser uma data futura.");

            if (ChangeUserId == Guid.Empty || ChangeUserId == null)
                errors.Add("ChangeUserId é obrigatório.");

            if (string.IsNullOrWhiteSpace(ChangeUserName))
                errors.Add("ChangeUserName é obrigatório.");

            if (errors.Count != 0)
                throw new ArgumentException($"Erros de validação na atualização: {string.Join(" ", errors)}");
        }

        private void ValidateForDeactivation()
        {
            var errors = new List<string>();

            if (ExclusionUserId == Guid.Empty || ExclusionUserId == null)
                errors.Add("ExclusionUserId é obrigatório.");

            if (string.IsNullOrWhiteSpace(ExclusionUserName))
                errors.Add("ExclusionUserName é obrigatório.");

            if (ExclusionDate == null)
                errors.Add("ExclusionDate é obrigatório.");

            if (errors.Count != 0)
                throw new ArgumentException($"Erros de validação na desativação: {string.Join(" ", errors)}");
        }

        public void ValidateFullEntity()
        {
            var errors = new List<string>();

            
            if (Id == Guid.Empty)
                errors.Add("Id é obrigatório.");

            if (Code < 0)
                errors.Add("Code não pode ser negativo.");

            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(AccessKey))
                errors.Add("AccessKey é obrigatória.");

            if (string.IsNullOrWhiteSpace(Password))
                errors.Add("Password é obrigatória.");

            if (!string.IsNullOrWhiteSpace(CPF) && !Validation.CPFValido(CPF))
                errors.Add("CPF inválido.");
                       

            if (AccountId == Guid.Empty || AccountId == null)
                errors.Add("AccountId é obrigatório.");

            if (string.IsNullOrWhiteSpace(AccountName))
                errors.Add("AccountName é obrigatório.");

            if (CreationDate == null)
                errors.Add("CreationDate é obrigatório.");

            if (CreationUserId == Guid.Empty || CreationUserId == null)
                errors.Add("CreationUserId é obrigatório.");

            if (string.IsNullOrWhiteSpace(CreationUserName))
                errors.Add("CreationUserName é obrigatório.");

            //if (Roles == null || !Roles.Any())
            //    errors.Add("Pelo menos uma role é obrigatória.");

            if (errors.Count != 0)
                throw new ArgumentException($"Erros de validação completa da entidade: {string.Join(" ", errors)}");
        }

        public enum UserStatus
        {
            Pending = 0,
            Active = 1,
            Disabled = 2
        }

/**/