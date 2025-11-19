namespace ConnectApp.Application.DTOs.Accounts
{

    public class AccountParams
    {
        public Guid? AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string? TemaPadrao { get; set; }
        public string? UrlLogo { get; set; }
        public string? UrlIcone { get; set; }
        public string? UrlImagemLogin { get; set; }
        public string? UrlImagemDashboard { get; set; }

        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public bool? RecordStatus { get; set; }
    }
}
