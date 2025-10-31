using ConnectApp.Domain.Entities.Accounts;

namespace ConnectApp.Application.DTOs.Accounts
{
    public class AccountResult
    {
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;

        public Guid? AccountId { get; set; }
        public string? AccountName { get; set; }
        public bool? Ativa { get; set; }

        public static AccountResult Success(string message, Account? entity = null)
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
    }
}
