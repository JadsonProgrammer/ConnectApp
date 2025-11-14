using ConnectApp.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Application.Services.Users
{
    public class UserValidationService
    {
        public static (bool isValid, string message) ValidateCreate(UserParams p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
                return (false, "Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(p.AccessKey))
                return (false, "AccessKey é obrigatória.");

            if (string.IsNullOrWhiteSpace(p.Password))
                return (false, "Senha é obrigatória.");

            return (true, string.Empty);
        }

        public (bool isValid, string message) ValidateUpdate(UserParams p)
        {
            if (string.IsNullOrWhiteSpace(p.Name))
                return (false, "Nome é obrigatório.");

            return (true, string.Empty);
        }
    }
}

