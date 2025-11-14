using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    public class Bkp
    {
        public int Id { get; set; }
        public Bkp()
        {
                
        }

        /*
         * namespace ConnectApp.Domain.Entities.Users
{
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

            public IList<string> Roles { get; set; } = new List<string> { "User" };
            public IList<Phone> Phones { get; set; } = new List<Phone>();
            public IList<Address> Addresses { get; set; } = new List<Address>();
            public IList<Email> Emails { get; set; } = new List<Email>();

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
            public bool IsActive { get; set; }
            public string? Note { get; set; }

            // Audit
            public Guid? BrokerId { get; set; }
            public Guid? AccountId { get; set; }
            public string AccountName { get; set; } = string.Empty;
            public DateTime? CreationDate { get; set; }
            public Guid? CreationUserId { get; set; }
            public string? CreationUserName { get; set; }
            public DateTime? ChangeDate { get; set; }
            public Guid? ChangeUserId { get; set; }
            public string? ChangeUserName { get; set; }
            public DateTime? ExclusionDate { get; set; }
            public Guid? ExclusionUserId { get; set; }
            public string? ExclusionUserName { get; set; }

            public User() { }





            public static User Create(
                string name,
                string? cpf,
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
                if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório.");
                if (string.IsNullOrWhiteSpace(accessKey)) throw new ArgumentException("AccessKey é obrigatória.");
                if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password é obrigatória.");

                return new User
                {
                    Id = Guid.NewGuid(),
                    Name = name.Trim(),
                    CPF = cpf?.Trim(),
                    AccessKey = accessKey,
                    Password = password,
                    Phones = phones ?? new List<Phone>(),
                    Addresses = addresses ?? new List<Address>(),
                    Emails = emails ?? new List<Email>(),
                    Roles = roles != null && roles.Any() ? roles : new List<string> { "User" },
                    AccountId = accountId,
                    AccountName = accountName,
                    IsActive = true,
                    RecordStatus = true,
                    CreationDate = DateTime.UtcNow,
                    CreationUserId = creationUserId,
                    CreationUserName = creationUserName
                };
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
                Phones = phones ?? Phones;
                Addresses = addresses ?? Addresses;
                Emails = emails ?? Emails;
                Avatar = avatar;
                Note = note;
                ChangeDate = DateTime.UtcNow;
                ChangeUserId = changeUserId;
                ChangeUserName = changeUserName;
            }

            public void Deactivate(Guid exclusionUserId, string exclusionUserName)
            {
                RecordStatus = false;
                IsActive = false;
                ExclusionDate = DateTime.UtcNow;
                ExclusionUserId = exclusionUserId;
                ExclusionUserName = exclusionUserName;
                ValidateForDeactivation();
            }
        }

    }
}






















































































/*
public partial class User
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public string Name { get; set; } = null!;
    public string? CPF { get; set; }
    public string AccessKey { get; set; } = null!;
    public string Password { get; set; } = null!;



    public IList<string> Roles { get; set; } = ["User"];
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
    public bool IsActive { get; set; }
    public string? Note { get; set; }


    //---------------- Audit Fields ----------------//
    public Guid? BrokerId { get; set; }
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public DateTime? CreationDate { get; set; }
    public Guid? CreationUserId { get; set; }
    public string? CreationUserName { get; set; }
    public DateTime? ChangeDate { get; set; }
    public Guid? ChangeUserId { get; set; }
    public string? ChangeUserName { get; set; }
    public DateTime? ExclusionDate { get; set; }
    public Guid? ExclusionUserId { get; set; }
    public string? ExclusionUserName { get; set; }


    //---------------------------------------------//


    public User() { }



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
        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            CPF = cpf?.Trim(),
            AccessKey = accessKey,
            Password = password, 
            Phones = phones ?? [],
            Addresses = addresses ?? [],
            Emails = emails ?? [],
            Roles = roles ?? ["User"],
            Id = accountId,
            Name = accountName,
            IsActive = true,
            RecordStatus = true,
            CreationDate = DateTime.UtcNow,
            CreationUserId = creationUserId,
            CreationUserName = creationUserName
        };
    }
    public void Update(userParams)
    {

    }
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
        Name = name.Trim();
        CPF = cpf?.Trim();
        Phones = phones ?? Phones;
        Addresses = addresses ?? Addresses;
        Emails = emails ?? Emails;
        Avatar = avatar;
        Note = note;
        ChangeDate = DateTime.UtcNow;
        ChangeUserId = changeUserId;
        ChangeUserName = changeUserName;
    }

    public void Deactivate(Guid userId, string userName)
    {
        RecordStatus = false;
        ExclusionDate = DateTime.UtcNow;
        ExclusionUserId = userId;
        ExclusionUserName = userName;
    }

}
}


*/


    }
}
