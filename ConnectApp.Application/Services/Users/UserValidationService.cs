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
        public static (bool isValid, string message) ValidateCreate(UserParams @params)
        {
            if (string.IsNullOrWhiteSpace(@params.Name))
                return (false, "Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(@params.AccessKey))
                return (false, "AccessKey é obrigatória.");

            if (string.IsNullOrWhiteSpace(@params.Password))
                return (false, "Senha é obrigatória.");

            return (true, string.Empty);
        }

        public static (bool isValid, string message) ValidateUpdate(UserParams @params)
        {
            if (string.IsNullOrWhiteSpace(@params.Name))
                return (false, "Nome é obrigatório.");

            return (true, string.Empty);
        }
    }
}

