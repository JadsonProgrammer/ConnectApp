using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Domain.Entities.Users
{
    public class Phone
    {
        public string Number { get; private set; }

        public Phone(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Número de telefone é obrigatório.");

            Number = new string(number.Where(char.IsDigit).ToArray());

            if (Number.Length < 8)
                throw new ArgumentException("Número de telefone é inválido.");
        }
    }
}