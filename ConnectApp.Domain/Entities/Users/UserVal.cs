using ConnectApp.Shared.Helpers;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{

        public partial class User
        {
            
            public static User Create(
                string name,
                string? cpf  ,
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
                ValidateName(name);
                ValidateAccessKey(accessKey);
                ValidatePassword(password);
                var Cpf = cpf?.Replace(".", "").Replace("-", "") ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(Cpf)) Validation.CPFValido(Cpf);

            return new User
                {
                    Id = Guid.NewGuid(),
                    Name = name.Trim(),
                    CPF = Cpf?.Trim(),
                    AccessKey = accessKey,
                    Password = password,
                    Phones = phones ?? [],
                    Addresses = addresses ?? [],
                    Emails = emails ?? [],
                    Roles = roles != null && roles.Any() ? roles : ["User"],
                    AccountId = accountId,
                    AccountName = accountName,                   
                    RecordStatus = true,
                    CreationDate = DateTime.UtcNow,
                    CreationUserId = creationUserId,
                    CreationUserName = creationUserName
                };
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
                ValidateName(name);
            var Cpf = cpf?.Replace(".", "").Replace("-", "") ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(Cpf)) Validation.CPFValido(Cpf);
           

                Name = name.Trim();
                CPF = Cpf?.Trim();
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
               
                ExclusionDate = DateTime.UtcNow;
                ExclusionUserId = exclusionUserId;
                ExclusionUserName = exclusionUserName;
            }

            
            private static void ValidateName(string name)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Nome é obrigatório.");
                if (name.Length > 250)
                    throw new ArgumentException("Nome muito longo.");
            }

            private static void ValidateAccessKey(string accessKey)
            {
                if (string.IsNullOrWhiteSpace(accessKey))
                    throw new ArgumentException("AccessKey é obrigatória.");
                if (accessKey.Length > 100) throw new ArgumentException("AccessKey inválida.");
            }

            private static void ValidatePassword(string password)
            {
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Password é obrigatória.");
                if (password.Length < 6) throw new ArgumentException("Password deve ter ao menos 6 caracteres.");
            }

            //private static void ValidateCPF(string cpf)
            //{
            //    // Simplified CPF format check; you can replace with robust algorithm.
            //    var digitsOnly = Regex.Replace(cpf ?? "", "[^0-9]", "");
            //    if (digitsOnly.Length != 11) throw new ArgumentException("CPF inválido.");
               
            //}
        }
    
}
/*
    public static User CreateUserDraft(string name, string cpf, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(cpf))
                throw new Exception("CPF é obrigatório");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(phone))
                throw new Exception("Telefone é obrigatório");

            return new User
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                CPF = cpf.Trim(),


                Status = UserStatus.Pending
            };
        }

        public void AttachAccount(Guid accountId)
        {
            if (accountId == Guid.Empty)
                throw new Exception("AccountId inválido");

            AccountId = accountId;
            Status = UserStatus.Active;
        }
    }



}
/**/