using ConnectApp.Domain.Entities.Accounts;

namespace ConnectApp.Application.DTOs.Accounts
{
    public class AccountResult {
        public bool Successo { get; set; }
        public string? Message { get; set; }
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

        // Métodos estáticos para criar resultados
        public static AccountResult Success(string message)
        {
            return new AccountResult
            {
                Successo = true,
                Message = message
            };
        }

        public static AccountResult Failure(string message)
        {
            return new AccountResult
            {
                Successo = false,
                Message = message
            };
        }
    }
























    /*
    {
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;

        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public bool? Ativa { get; set; }
        public int Code { get; set; }

        public static AccountResult Success(string message, AccountResult? entity = null)
        {
            return new AccountResult
            {
                Error = false,
                Message = message,
                AccountId = entity?.AccountId,
                AccountName = entity?.AccountName,
                Ativa = entity?.Ativa
            };
        }

        public static AccountResult Failure(string message)
        {
            return new AccountResult
            {
                Error = true,
                Message = message
            };
        }
    /*/
}

