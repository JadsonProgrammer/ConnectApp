using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Application.DTOs.Auths
{

    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;
        public DateTime? Expires { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; } = string.Empty;

        public static AuthResult Success(string token, DateTime expires, string message = "")
            => new AuthResult { Token = token, Expires = expires, Message = message, Error = false };

        public static AuthResult Failure(string message)
            => new AuthResult { Error = true, Message = message };
    }

}

