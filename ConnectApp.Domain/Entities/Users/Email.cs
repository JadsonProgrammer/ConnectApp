using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    public class Email
    {
        public int Code { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = Guid.Empty;
        public byte IsPrimary { get; set; }
        public string? EnderecoEletronico { get; set; }
        public string? Note { get; set; }
        public bool? RecordStatus { get; set; }
        public Guid? BrokerId { get; set; } = Guid.Empty;
        public Guid? AccountId { get; set; } = Guid.Empty;
        public string? AccountName { get; set; } = string.Empty;
        public DateTime? CreationDate { get; set; }
        public Guid? CreationUserId { get; set; } = Guid.Empty;
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid? ChangeUserId { get; set; } = Guid.Empty;
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid? ExclusionUserId { get; set; } = Guid.Empty;
        public string? ExclusionUserName { get; set; }        
        
        public string Value { get; private set; }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório.");

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Email inválido.");

            Value = email.Trim();
        }
    }
}
