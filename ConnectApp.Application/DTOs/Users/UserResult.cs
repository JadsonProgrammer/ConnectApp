namespace ConnectApp.Application.DTOs.Users
{
    public class UserResult
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; } = null!;
        public bool Erro { get; set; }
        public string? Message { get; set; } = null!;
        public UserResult() { }
    }
}
