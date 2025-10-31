using ConnectApp.Shared.Results;

namespace ConnectApp.Domain.Entities.Accounts
{
    public class Account
    {
        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; } = string.Empty;
        public bool Ativa { get; private set; } = true;
        public string? TemaPadrao { get; private set; }
        public string? UrlLogo { get; private set; }
        public string? UrlIcone { get; private set; }
        public string? UrlImagemLogin { get; private set; }
        public string? UrlImagemDashboard { get; private set; }

        // Auditoria
        public DateTime CreationDate { get; private set; }
        public Guid CreationUserId { get; private set; }
        public string? CreationUserName { get; private set; }
        public DateTime? ChangeDate { get; private set; }
        public Guid? ChangeUserId { get; private set; }
        public string? ChangeUserName { get; private set; }
        public DateTime? ExclusionDate { get; private set; }
        public Guid? ExclusionUserId { get; private set; }
        public string? ExclusionUserName { get; private set; }
        public bool RecordStatus { get; private set; } = true;

        private Account() { }

        // 🔹 Fábrica pura (sem Result)
        public static Account Create(
            string accountName,
            Guid creationUserId,
            string? creationUserName = null,
            string? temaPadrao = null,
            string? urlLogo = null,
            string? urlIcone = null,
            string? urlImagemLogin = null,
            string? urlImagemDashboard = null)
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
