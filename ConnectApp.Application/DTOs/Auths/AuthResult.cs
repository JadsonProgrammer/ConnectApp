namespace ConnectApp.Application.DTOs.Auths
{
    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;

        // Novas propriedades necessárias
        public Guid? UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid? AccountId { get; set; }
        public string AccountName { get; set; } = string.Empty;

        public static AuthResult Success(string token, DateTime expires, Guid userId, string userName, Guid? accountId = null, string accountName = "", string message = "")
            => new()
            {
                Token = token,
                Expires = expires,
                Message = message,
                Error = false,
                UserId = userId,
                UserName = userName,
                AccountId = accountId,
                AccountName = accountName
            };

        public static AuthResult Failure(string message, Guid? userId = null, string userName = "")
            => new()
            {
                Error = true,
                Message = message,
                UserId = userId,
                UserName = userName
            };

        // Método adicional para sucesso no SignUp
        public static AuthResult SignUpSuccess(Guid userId, string userName, string message = "Usuário criado com sucesso")
            => new()
            {
                Error = false,
                Message = message,
                UserId = userId,
                UserName = userName
            };
    }
}