using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Services.Auths
{
    public class AuthService(
        IJwtTokenService tokenService,
        IUserRepository userRepository,
        IAuthRepository authRepository)
        : ResultService, IAuthService
    {
        private readonly IJwtTokenService _tokenService = tokenService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<bool> LoginExistsAsync(AuthParams @params)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(@params.AccessKey);
            return existingUser.Error == false;
        }

        public async Task<AuthResult> SignUpAsync(UserParams @params)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByUsernameAsync(@params.AccessKey);

                if (existingUser.Error == true)
                {
                    AddMessage(new ResultMessage
                    {
                        Text = "Usuário já existe.",
                        Code = "400",
                        Type = ResultMessageTypes.Error
                    });

                    return new AuthResult
                    {
                        Error = true,
                        Message = "Login já pertence a outro usuário"
                    };
                }

                // O Hash agora é feito via _tokenService
                var userReady = UserMapper.MapForInsert(userCreate, _tokenService.HashPassword);

                var result = await _userRepository.CreatesUserAsync(userReady);
                return result;
            }
            catch (Exception ex)
            {
                AddMessage(new ResultMessage
                {
                    Code = "500",
                    Text = $"Erro ao criar usuário: {ex.Message}",
                    Type = ResultMessageTypes.Error
                });
                return new AuthResult
                {
                    Error = true,
                    Message = "Erro interno ao criar usuário"
                };
            }
        }

        public async Task<AuthResult> SignInAsync(AuthParams @params)
        {
            try
            {
                var user = await _authRepository.GetByUserAsync(@params);

                if (user == null)
                    return InvalidLogin("Usuário ou senha inválidos.");

                // Valida via service
                if (!_tokenService.VerifyPassword(@params.Password, user.Password))
                    return InvalidLogin("Usuário ou senha inválidos.");

                var authResult = GenerateAuthResult(user);

                AddMessage(new ResultMessage
                {
                    Code = "200",
                    Text = "Usuário autenticado com sucesso.",
                    Type = ResultMessageTypes.Success
                });

                return new AuthResult
                {
                    Token = authResult.Token,
                    Expires = authResult.Expires,
                    Message = "Usuário logado com sucesso!"
                };
            }
            catch (Exception ex)
            {
                AddMessage(new ResultMessage
                {
                    Code = "500",
                    Text = $"Erro inesperado: {ex.Message}",
                    Type = ResultMessageTypes.Error
                });

                return new AuthResult
                {
                    Error = true,
                    Message = "Ocorreu um erro ao tentar autenticar. Tente novamente."
                };
            }
        }

        private AuthResult InvalidLogin(string message)
        {
            AddMessage(new ResultMessage
            {
                Code = "403",
                Text = message,
                Type = ResultMessageTypes.Error
            });

            return new AuthResult
            {
                Error = true,
                Message = message
            };
        }

        private AuthResult GenerateAuthResult(User user)
        {
            var token = _tokenService.GenerateToken(user);
            return new AuthResult
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
