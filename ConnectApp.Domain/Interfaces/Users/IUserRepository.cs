using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Domain.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByUsernameAsync(string username);       
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateAsync(User user);
    }
}
