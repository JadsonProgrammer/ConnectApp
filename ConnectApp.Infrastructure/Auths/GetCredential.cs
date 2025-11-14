using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ConnectApp.Infrastructure.Auths
{
    public class GetCredential : IGetCredential
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private User? _cachedUser;

        public GetCredential(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        private async Task<User?> GetUserFromDbAsync()
        {
            // Retorna do cache se disponível
            if (_cachedUser != null)
                return _cachedUser;

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.User?.Identity?.IsAuthenticated != true)
                return null;

            // Tenta obter o userId do token JWT
            var userIdClaim = httpContext.User.FindFirst("userId")?.Value
                           ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return null;

            try
            {
                _cachedUser = await _userRepository.GetUserByIdAsync(userId);
                return _cachedUser;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Guid> GetUserIdAsync()
        {
            var user = await GetUserFromDbAsync();
            return user?.Id ?? Guid.Empty;
        }

        public async Task<string> GetUserNameAsync()
        {
            var user = await GetUserFromDbAsync();
            return user?.Name ?? string.Empty;
        }

        public async Task<Guid> GetAccountIdAsync()
        {
            var user = await GetUserFromDbAsync();
            return user?.AccountId ?? Guid.Empty;
        }

        public async Task<string> GetAccountNameAsync()
        {
            var user = await GetUserFromDbAsync();
            return user?.AccountName ?? string.Empty;
        }

        public async Task<User?> GetUserAsync()
        {
            return await GetUserFromDbAsync();
        }
    }
}