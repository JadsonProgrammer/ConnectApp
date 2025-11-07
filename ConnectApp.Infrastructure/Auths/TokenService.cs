using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConnectApp.Infrastructure.Auths
{

    public class JwtTokenService : IJwtTokenService
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var claims = new List<Claim>
            {
                new ("userId", user.Id.ToString()),
                new ("sessionId", Guid.NewGuid().ToString())
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        // concatena password + Salt + Secret e aplica BCrypt (recomendado evitar secret global, mas faço conforme pediu)
        public string HashPassword(string password)
        {
            var composite = string.Concat(password ?? string.Empty, Settings.Salt, Settings.Secret);
            return BCrypt.Net.BCrypt.HashPassword(composite);
        }

        // verifica recebendo a senha em texto; concatena, aplica Verify
        public bool VerifyPassword(string password, string storedHash)
        {
            var composite = string.Concat(password ?? string.Empty, Settings.Salt, Settings.Secret);
            return BCrypt.Net.BCrypt.Verify(composite, storedHash);
        }
    }
}

/*
public static string GenerateToken(string userId, string sessionId, out DateTime expires)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(Settings.Secret);
    expires = DateTime.UtcNow.AddDays(1);

    var descriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(
        [
        new Claim("UserId", userId),
            new Claim("SessionId", sessionId)
    ]),
        Expires = expires,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(descriptor);
    return tokenHandler.WriteToken(token);
}

*/
