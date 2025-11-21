using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Shared.Helpers;
using System.Data;

namespace ConnectApp.Application.DTOs.Users
{
    public class UserParams
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IList<Phone>? Phones { get; set; } = [];
        public IList<Address>? Addresses { get; set; } = [];
        public IList<Email>? Emails { get; set; } = [];
        public string? Avatar { get; set; }
        public string? Note { get; set; }
        //public IList<string>? Roles { get; set; } 


       

           
        



        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(CPF))
                throw new ArgumentException("CPF é obrigatório");

            if (Emails == null || !Emails.Any())
                throw new ArgumentException("Email é obrigatório");

            foreach (var email in Emails)
            {
                if (string.IsNullOrWhiteSpace(email.Value))
                    throw new ArgumentException("Valor do email não pode ser vazio");

                if (!email.Value.Contains('@'))
                    throw new ArgumentException($"Email inválido: {email.Value}");
            }

            if (Phones == null || !Phones.Any())
                throw new ArgumentException("Telefone é obrigatório");

            foreach (var phone in Phones)
            {
                if (phone.Telefone <= 0)
                    throw new ArgumentException("Número de telefone deve ser positivo");

                var phoneString = phone.Telefone.ToString();

                if (phoneString.Length < 8 || phoneString.Length > 15)
                    throw new ArgumentException($"Número deve ter entre 8 e 15 dígitos: {phone.Telefone}");
            }

            if (!Validation.CPFValido(CPF))
                throw new ArgumentException("CPF inválido");

            if (Emails != null)
            {
                foreach (var email in Emails)
                {

                    if (email.Value != null && !email.Value.Contains('@'))
                        throw new ArgumentException($"Email inválido: {email.Value}");
                }
            }
        }
    }
}



