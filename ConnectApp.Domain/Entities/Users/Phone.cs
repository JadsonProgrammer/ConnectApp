using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    public class Phone
    {
        public int Code { get; set; }
        public Guid Id { get; set; }
        public byte DDD { get; set; }
        public long Telefone { get; set; }
        public string? Note { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsWhatsApp { get; set; }
        public byte? RecordStatus { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public Guid AccountId { get; set; }
        public string? AccountName { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? CreationUserId { get; set; }
        public string? CreationUserName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public Guid? ChangeUserId { get; set; }
        public string? ChangeUserName { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public Guid? ExclusionUserId { get; set; }
        public string? ExclusionUserName { get; set; }
        public Phone(byte ddd, string numeroTelefone)
        {

            if (ddd <= 0 || ddd > 99)
            {
                throw new ArgumentException("O DDD deve ser um número válido entre 1 e 99.");
            }
            if (string.IsNullOrWhiteSpace(numeroTelefone))
            {
                throw new ArgumentException("Número de telefone é obrigatório.");
            }
            string numeroLimpo = new(numeroTelefone.Where(char.IsDigit).ToArray());


            if (numeroLimpo.Length < 8 || numeroLimpo.Length > 9)
            {
                throw new ArgumentException("Número de telefone é inválido. Deve conter 8 ou 9 dígitos.");
            }


            DDD = ddd;

            if (!long.TryParse(numeroLimpo, out long numeroConvertido))
            {

                throw new ArgumentException("O número de telefone fornecido não é um número válido.");
            }
            Telefone = numeroConvertido;
        }
    }
}