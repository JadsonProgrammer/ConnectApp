using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Infrastructure.Auth
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, string salt, string secret)
        {
            var combined = $"{password}{salt}{secret}";
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string inputPassword, string storedHash, string salt, string secret)
        {
            var hashOfInput = HashPassword(inputPassword, salt, secret);
            return hashOfInput == storedHash;
        }
    }
}
