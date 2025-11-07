using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Infrastructure.Auth.Token;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace ConnectApp.Infrastructure.Auths.Token
{


    public class JwtTokenService
    {
        /*
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateToken(User user)
        {
            // usa _settings.Secret, _settings.Issuer, etc
            return "jwt-token-gerado";
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var input = $"{password}{_settings.Salt}{_settings.Secret}";
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hash = HashPassword(inputPassword);
            return hash == storedHash;
        }
    }

    //public class JwtTokenService : IJwtTokenService
    //{
    //    private readonly JwtSettings _jwtSettings;

    //    public JwtTokenService(JwtSettings jwtSettings)
    //    {
    //        _jwtSettings = jwtSettings;
    //    }

    //    public string GenerateToken(User user)
    //    {
    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var now = DateTime.UtcNow;

    //        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

    //        var claims = new List<Claim>
    //    {

    //        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    //        new Claim("userId", user.Id.ToString()),






    //    };
    //        if (user.AccountId.HasValue)
    //        {
    //            claims.Add(new Claim("accountId", user.AccountId.Value.ToString()));
    //        }

    //        if (!string.IsNullOrEmpty(user.AccountName))
    //        {
    //            claims.Add(new Claim("accountName", user.AccountName));
    //        }

    //        /*if (user.Roles != null && user.Roles.Any())
    //        {
    //            foreach (var role in user.Roles)
    //            {
    //                claims.Add(new Claim(ClaimTypes.Role, role));
    //            }
    //        }
    //        */

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(claims),
        //            NotBefore = now,
        //            Expires = now.AddHours(_jwtSettings.ExpiryInHours),

        //            Issuer = _jwtSettings.Issuer,
        //            Audience = _jwtSettings.Audience,
        //            SigningCredentials = new SigningCredentials(
        //                new SymmetricSecurityKey(key),
        //                SecurityAlgorithms.HmacSha512Signature)
        //        };

        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return tokenHandler.WriteToken(token);
        //    }
        //}
    }
}