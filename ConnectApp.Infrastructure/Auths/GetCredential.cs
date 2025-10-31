using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ConnectApp.Infrastructure.Auth
{
    public class GetCredential : IGetCredential
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        private User? _cachedUser; // cache local para não bater no banco várias vezes

        public GetCredential(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            if (_cachedUser != null) return _cachedUser;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userIdClaim)) return null;

            if (!Guid.TryParse(userIdClaim, out var userId)) return null;

            _cachedUser = await _userRepository.GetUserByIdAsync(userId);
            return _cachedUser;
        }

        public Guid GetUserId()
        {
            var userTask = GetCurrentUserAsync();
            userTask.Wait();
            return userTask.Result?.Id ?? Guid.Empty;
        }

        public string GetUserName()
        {
            var userTask = GetCurrentUserAsync();
            userTask.Wait();
            return userTask.Result?.Name ?? string.Empty;
        }

        public Guid GetAccountId()
        {
            var userTask = GetCurrentUserAsync();
            userTask.Wait();
            return userTask.Result?.AccountId ?? Guid.Empty;
        }

        public string GetAccountName()
        {
            var userTask = GetCurrentUserAsync();
            userTask.Wait();
            return userTask.Result?.AccountName ?? string.Empty;
        }
    }
}
