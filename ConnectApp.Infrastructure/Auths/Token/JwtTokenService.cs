using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ConnectApp.Infrastructure.Auths.Token
{


    public class JwtTokenService : IJwtTokenService
    {

        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var expires = DateTime.UtcNow.AddHours(_settings.ExpiryInHours);

            // 💡 Onde o erro estava: a lista de claims deve incluir Issuer e Audience
            var claims = new List<Claim>
            {
                new("UserId", user.Id.ToString()),
                new(JwtRegisteredClaimNames.Aud, _settings.Audience),
                new(JwtRegisteredClaimNames.Iss, _settings.Issuer)
            };

            var descriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }


        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var input = $"{password}{_settings.Salt}";
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hash = HashPassword(inputPassword);
            return hash == storedHash;
        }
    }


}
/*


  var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;

            var key = Encoding.UTF8.GetBytes(_settings.Secret);

            var claims = new List<Claim>
        {

            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("userId", user.Id.ToString()),






        };
           

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = now,
                Expires = now.AddHours(_settings.ExpiryInHours),

                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

/**/