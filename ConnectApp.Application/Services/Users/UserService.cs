using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Services.Users
{
    public class UserService : ResultService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResultMessage> CreatesUserAsync(UserParams user)
        {
            try
            {
                var _user = await this._userRepository.CreateUserAsync(user);
                return new ResultMessage
                {
                    Code = "200",
                    Text = "Usuário criado com sucesso",
                    Type = ResultMessageTypes.Success
                };
            }
            catch (Exception ex)
            {
                AddMessage(new ResultMessage
                {
                    Code = "400",
                    Text = "Erro ao criar usuário",
                    Type = ResultMessageTypes.Error
                });
                return new ResultMessage
                {
                    Code = "400",
                    Text = $"{ex.Message}",
                    Type = ResultMessageTypes.Error
                };
            }
        }
        public async Task<UserResult> CreateAsync(User user)
        {
            try
            {

                var _user = await this._userRepository.CreateUserAsync(user);

                return _user;
            }
            catch (Exception)
            {
                AddMessage(new ResultMessage
                {
                    Code = "400",
                    Text = "Nenhum usuário localizado nessa conta",
                    Type = ResultMessageTypes.Error
                });
                //var userResult = new UserResult
                //{
                //    Erro = false,
                //    Message = $"{ex}"
                //};
                var userResult = new UserResult
                {

                };

                return userResult;


            }
        }
        public Task<UserResult> LoginAsync(string accessKey, string password)
        {
            throw new NotImplementedException();
        }
        public async Task<List<User>> GetAllAsync()
        {
            try
            {

                var users = await this._userRepository.GetAllUserAsync();
                return users;
            }
            catch (Exception)
            {
                AddMessage(new ResultMessage
                {
                    Code = "400",
                    Text = "Nenhum ussário localizado nessa conta",
                    Type = ResultMessageTypes.Error
                });
                return [];
            }
        }
        public async Task<User> GetUserByIdAsync(UserParams userParams)
        {
            try
            {

                var users = await this._userRepository.GetUserByIdAsync(userParams.Id);
                return users;
            }
            catch (Exception)
            {
                AddMessage(new ResultMessage
                {
                    Code = "400",
                    Text = "Nenhum usuário localizado nessa conta",
                    Type = ResultMessageTypes.Error
                });
                return null!;
            }
        }



        public async Task<bool> UpdateUserByIdAsync(User user)
        {

            try
            {

                var users = await this._userRepository.UpdateAsync(user);
                return users;
            }
            catch (Exception)
            {
                AddMessage(new ResultMessage
                {
                    Code = "400",
                    Text = "Nenhum usuário localizado nessa conta",
                    Type = ResultMessageTypes.Error
                });
                return false;
            }
        }






        public async Task<Credentials> GetCredentialByIdAsync(Guid userId)
        {

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;


            return new Credentials
            {
                UserId = user.Id,
                UserName = user.Name,
                AccountId = user.AccountId,
                AccountName = user.AccountName,
            };
        }
    }
}
