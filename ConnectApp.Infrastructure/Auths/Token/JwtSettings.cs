using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Infrastructure.Auths.Token
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public int ExpiryInHours { get; set; } = 2;
        public string Issuer { get; set; }  = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public int RefreshTokenExpiryInDays { get; set; } = 7;
    }
}
