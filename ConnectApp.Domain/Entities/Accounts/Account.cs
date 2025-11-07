using ConnectApp.Shared.Results;

namespace ConnectApp.Domain.Entities.Accounts
{
    public class Account
    {
        public Guid AccountId { get; set; } 
        public string AccountName { get; set; } = string.Empty;
        public bool Ativa { get; set; }

        // Personalização
        public string? TemaPadrao { get; set; }
        public string? UrlLogo { get; set; }
        public string? UrlIcone { get; set; }
        public string? UrlImagemLogin { get; set; }
        public string? UrlImagemDashboard { get; set; }

        // Auditoria
        public DateTime? CreationDate { get; set; }
        public Guid? CreationUserId { get; set; }
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid? ChangeUserId { get; set; }
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid? ExclusionUserId { get; set; }
        public string? ExclusionUserName { get; set; }
        public bool RecordStatus { get; set; }

        public Account() { }

        
        public static Account Create(
            string accountName,
            Guid creationUserId,
            string creationUserName,
            string? temaPadrao,
            string? urlLogo ,
            string? urlIcone,
            string? urlImagemLogin,
            string? urlImagemDashboard)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                throw new ArgumentException("O nome da conta é obrigatório.");

            if (creationUserId == Guid.Empty)
                throw new ArgumentException("O usuário de criação é obrigatório.");

            return new Account
            {
                AccountId = Guid.NewGuid(),
                AccountName = accountName.Trim(),
                Ativa = true,
                TemaPadrao = string.IsNullOrWhiteSpace(temaPadrao) ? "default" : temaPadrao,
                UrlLogo = urlLogo,
                UrlIcone = urlIcone,
                UrlImagemLogin = urlImagemLogin,
                UrlImagemDashboard = urlImagemDashboard,
                CreationDate = DateTime.UtcNow,
                CreationUserId = creationUserId,
                CreationUserName = creationUserName ?? "Sistema",
                RecordStatus = true
            };
        }

        public void Update(
            string accountName,
            string? temaPadrao,
            string? urlLogo,
            string? urlIcone,
            string? urlImagemLogin,
            string? urlImagemDashboard,
            Guid changeUserId,
            string? changeUserName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
                throw new ArgumentException("O nome da conta é obrigatório para atualização.");

            AccountName = accountName.Trim();
            TemaPadrao = temaPadrao;
            UrlLogo = urlLogo;
            UrlIcone = urlIcone;
            UrlImagemLogin = urlImagemLogin;
            UrlImagemDashboard = urlImagemDashboard;
            ChangeDate = DateTime.UtcNow;
            ChangeUserId = changeUserId;
            ChangeUserName = changeUserName;
        }

        public void Deactivate(Guid userId, string userName)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("Usuário inválido para exclusão.");

            Ativa = false;
            RecordStatus = false;
            ExclusionDate = DateTime.UtcNow;
            ExclusionUserId = userId;
            ExclusionUserName = userName;
        }
    }
}
