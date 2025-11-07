using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ConnectApp.Shared.Credential
{
    public static class HttpContextAccessorExtensions
    {
        public static Credentials GetCredentials(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor?.HttpContext == null)
                throw new InvalidOperationException("HttpContext não disponível");

            var user = httpContextAccessor.HttpContext.User;

            if (user?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("Usuário não autenticado");

            return new Credentials
            {
                UserId = httpContextAccessor.GetGuidValue("userId"),
                UserName = httpContextAccessor.GetStringValue("Name"),
                AccountId = httpContextAccessor.GetGuidValue("accountId"),
                AccountName = httpContextAccessor.GetStringValue("accountName"),
                Roles = httpContextAccessor.GetRolesValue()
            };
        }

        public static Guid GetGuidValue(this IHttpContextAccessor httpContextAccessor, string claimType)
        {
            var claimValue = httpContextAccessor.GetStringValue(claimType);

            if (Guid.TryParse(claimValue, out var guid))
                return guid;

            throw new InvalidOperationException($"Claim '{claimType}' não contém um GUID válido: {claimValue}");
        }

        public static string GetStringValue(this IHttpContextAccessor httpContextAccessor, string claimType)
        {
            var claim = httpContextAccessor.HttpContext?.User?.FindFirst(claimType);
            return claim?.Value ?? throw new InvalidOperationException($"Claim '{claimType}' não encontrada");
        }

        public static List<string> GetRolesValue(this IHttpContextAccessor httpContextAccessor)
        {
            var roles = httpContextAccessor.HttpContext?.User?
                .FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            return roles ?? [];
        }

        // Métodos opcionais para acesso individual (no mesmo estilo)
        public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.GetGuidValue("userId");
        }

        public static Guid GetAccountId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.GetGuidValue("accountId");
        }

        public static string GetUserName(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.GetStringValue("userName");
        }

        public static string GetAccountName(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.GetStringValue("accountName");
        }
    }
}
