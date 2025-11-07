using ConnectApp.Domain.Entities.Users;

namespace ConnectApp.Domain.Interfaces.Users
{
    public interface IUserRepository
    {

        //--------------Get------------------//
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IList<User>> GetAllUserAsync();
        Task<User?> GetUserByUsernameAsync(string username);

        //--------------Create------------------//
        Task<User> CreateUserAsync(User user);

        //--------------Update------------------//
        Task<User?> UpdateUserAsync(User user);



        //--------------Delete------------------//
        //Task<bool> DeleteUserAsync(User user);
    }
}
