namespace ConnectApp.Domain.Entities.Accounts
{
    public partial class Account
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
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

        
        //public static Account Create(
        //    string Name,
        //    Guid creationUserId,
        //    string? creationUserName,
        //    string? temaPadrao,
        //    string? urlLogo ,
        //    string? urlIcone,
        //    string? urlImagemLogin,
        //    string? urlImagemDashboard)
        //{
        //    if (string.IsNullOrWhiteSpace(Name))
        //        throw new ArgumentException("O nome da conta é obrigatório.");

        //    //if (creationUserId == Guid.Empty)
        //    //    throw new ArgumentException("O usuário de criação é obrigatório.");

        //    return new Account
        //    {
        //        Id = Guid.NewGuid(),
        //        Name =Name.Trim(),
        //        Ativa = true,
        //        TemaPadrao = string.IsNullOrWhiteSpace(temaPadrao) ? "default" : temaPadrao,
        //        UrlLogo = urlLogo,
        //        UrlIcone = urlIcone,
        //        UrlImagemLogin = urlImagemLogin,
        //        UrlImagemDashboard = urlImagemDashboard,
        //        CreationDate = DateTime.UtcNow,
        //        CreationUserId = creationUserId != Guid.Empty ? creationUserId : GetSystemUserId(),
        //        CreationUserName = GetCreationUserName(creationUserName),
        //        RecordStatus = true

        //    };
        //}
        //private static Guid GetSystemUserId()
        //{
            
        //    return Guid.Parse("00000000-0000-0000-0000-000000000000");
        //}
        //private static string GetCreationUserName(string? creationUserName)
        //{
        //    if (string.IsNullOrWhiteSpace(creationUserName))
        //        return "Sistema";

        //    return creationUserName.Trim();
        //}

        //public void Update(
        //    string Name,
        //    string? temaPadrao,
        //    string? urlLogo,
        //    string? urlIcone,
        //    string? urlImagemLogin,
        //    string? urlImagemDashboard,
        //    Guid changeUserId,
        //    string? changeUserName)
        //{
        //    if (string.IsNullOrWhiteSpace(Name))
        //        throw new ArgumentException("O nome da conta é obrigatório para atualização.");

        //    Name = Name.Trim();
        //    TemaPadrao = temaPadrao;
        //    UrlLogo = urlLogo;
        //    UrlIcone = urlIcone;
        //    UrlImagemLogin = urlImagemLogin;
        //    UrlImagemDashboard = urlImagemDashboard;
        //    ChangeDate = DateTime.UtcNow;
        //    ChangeUserId = changeUserId;
        //    ChangeUserName = changeUserName;
        //}

        //public void Deactivate(Guid userId, string userName)
        //{
        //    if (userId == Guid.Empty)
        //        throw new ArgumentException("Usuário inválido para exclusão.");

        //    Ativa = false;
        //    RecordStatus = false;
        //    ExclusionDate = DateTime.UtcNow;
        //    ExclusionUserId = userId;
        //    ExclusionUserName = userName;
        //}
    }
}
