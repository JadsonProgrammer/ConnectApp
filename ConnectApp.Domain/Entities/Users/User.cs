namespace ConnectApp.Domain.Entities.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public int? ProfileCode { get; set; }
        public string? ProfileName { get; set; }
        public int? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public DateTime? LastAccess { get; set; }
        public int? AccessCount { get; set; }
        public string? Avatar { get; set; }
        public string? Note { get; set; }
        public Guid? BrokerId { get; set; }
        public Guid? AccountId { get; set; }
        public string AccountName { get; set; }
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
        public string? CPF { get; set; }
        public int? IsActive { get; set; }

        public User() { }
    }
}