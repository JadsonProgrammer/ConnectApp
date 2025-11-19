using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Application.Results;
using ConnectApp.Domain.Interfaces.Auths;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ConnectApp.Application.Services.Auths
{
    public class AuthService : ResultService, IAuthService
    {
        private readonly IJwtTokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        //private readonly ILogger<AuthService> _logger;

        public AuthService(
            IJwtTokenService tokenService,
            IUserRepository userRepository,
            IAuthRepository authRepository,
            ILogger<AuthService> logger)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _authRepository = authRepository;
           //_logger = logger;
        }

        public async Task<Result<bool>> LoginExistsAsync(AuthCheck @params)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(@params.AccessKey))
                    return Failure<bool>("AccessKey não pode ser vazio");

                var existingUser = await _userRepository.GetUserByAccessKeyAsync(@params.AccessKey);

             
                bool loginExists = existingUser != null;
                string message = loginExists ? "Login já existe" : "Login disponível";

                return Success(loginExists, message);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro ao verificar existência do login para {AccessKey}", @params.AccessKey);
                return Failure<bool>("Erro ao verificar disponibilidade do login", ex.Message);
            }
        }

        public async Task<Result<AuthResult>> SignInAsync(AuthParams @params)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(@params.AccessKey) || string.IsNullOrWhiteSpace(@params.Password))
                    return Failure<AuthResult>("AccessKey e senha são obrigatórios");

                
                var existingUser = await _authRepository.GetByUserAsync(@params.AccessKey);
                if (existingUser is null)
                {
                   // _logger.LogWarning("Tentativa de login com AccessKey não encontrado: {AccessKey}", @params.AccessKey);
                    return Failure<AuthResult>("Usuário ou senha inválidos");
                }

                
                if (!_tokenService.VerifyPassword(@params.Password, existingUser.Password))
                {
                   // _logger.LogWarning("Tentativa de login com senha inválida para usuário: {UserId}", existingUser.Id);
                    return Failure<AuthResult>("Usuário ou senha inválidos");
                }

                
                if (!existingUser.IsActive)
                    return Failure<AuthResult>("Usuário inativo");

                if (!existingUser.RecordStatus)
                    return Failure<AuthResult>("Usuário excluído");

                
                var token = _tokenService.GenerateToken(existingUser);
                var expires = DateTime.UtcNow.AddHours(2);

                
                _ = Task.Run(async () => await UpdateLastAccessAsync(existingUser.Id));

                // _logger.LogInformation("Login realizado com sucesso para usuário: {UserId}", existingUser.Id);


                
                return Success(AuthResult.Success(
                    token: token,
                    expires: expires,
                    userId: existingUser.Id,
                    userName: existingUser.Name,
                    accountId: existingUser.AccountId,
                    accountName: existingUser.AccountName,
                    message: "Login realizado com sucesso"
                ));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Erro durante autenticação para AccessKey: {AccessKey}", @params.AccessKey);
                return Failure<AuthResult>($"Erro interno durante autenticação:  {ex}" );
            }
        }

        private async Task UpdateLastAccessAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user != null)
                {
                    user.LastAccess = DateTime.UtcNow;
                    user.AccessCount = (user.AccessCount ?? 0) + 1;
                    await _userRepository.UpdateUserAsync(user);

                   // _logger.LogDebug("Último acesso atualizado para usuário: {UserId}", userId);
                }
            }
            catch (Exception)
            {
                //_logger.LogError(ex, "Erro ao atualizar último acesso do usuário: {UserId}", userId);
                
            }
        }



       





    }
}


/*

public async Task<Result<AuthResult>> SignInAsync(AuthParams @params)
{
    try
    {
        var existingUser = await _authRepository.GetByUserAsync(@params.AccessKey);

        if (existingUser is null)
            return Failure<AuthResult>("Usuário ou senha inválidos.");

        // Valida a senha
        if (!_tokenService.VerifyPassword(@params.Password, existingUser.Password))
            return Failure<AuthResult>("Usuário ou senha inválidos.");

        // Verificar se o usuário está ativo
        if (!existingUser.IsActive || !existingUser.RecordStatus)
            return Failure<AuthResult>("Usuário inativo ou excluído.");

        var token = _tokenService.GenerateToken(existingUser);
        var expires = DateTime.UtcNow.AddHours(2);

        // Atualizar último acesso
        await UpdateLastAccess(existingUser.Id);



        return Success(AuthResult.Success(token: token,
                                  expires: expires,
                                  userId: existingUser.Id,
                                  userName: existingUser.Name,
                                  accountId: existingUser.AccountId,
                                  accountName: existingUser.AccountName,
                                  message: "Usuário logado com sucesso!"));
    }
    catch (Exception)
    {
        return Failure<AuthResult>("Ocorreu um erro ao tentar autenticar. Tente novamente.");
    }
}

private async Task UpdateLastAccess(Guid userId)
{
    try
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null)
        {
            user.LastAccess = DateTime.UtcNow;
            user.AccessCount = (user.AccessCount ?? 0) + 1;
            await _userRepository.UpdateUserAsync(user);
        }
    }
    catch (Exception)
    {
        Failure<AuthResult>("Ocorreu um erro ao atualizar o último acesso do usuário.");

    }
}





















public async Task<AuthResult> SignUpAsync(AuthParams @params)
{
    try
    {

        var existingUser = await _userRepository.GetUserByAccessKeyAsync(@params.AccessKey);
        if (existingUser != null)
        {
            AddMessage(new ResultMessage
            {
                Text = "Usuário já existe.",
                Code = "400",
                Type = ResultMessageTypes.Error
            });

            return AuthResult.Failure("Login já pertence a outro usuário");
        }
        var creationUserId = _httpContextAccessor.GetUserId();
        var creationUserName = _httpContextAccessor.GetUserName();
        var accountId = _httpContextAccessor.GetAccountId();
        var accountName = _httpContextAccessor.GetAccountName();

        // Se não estiver autenticado, usar valores padrão
        if (creationUserId == Guid.Empty)
        {
            creationUserId = Guid.NewGuid();
            creationUserName = "System";
            accountId = Guid.NewGuid();
            accountName = "Default Account";
        }
        // Gerar um nome baseado no AccessKey
        //var userName = GenerateUserNameFromAccessKey(@params.AccessKey);

        // Criar novo usuário
        var user = User.Create(
            name: @params.Name, // Usar nome gerado
            cpf: @params.CPF,
            accessKey: @params.AccessKey,
            password: _tokenService.HashPassword(@params.Password),
            phones: null,
            addresses: null,
            emails: null,
            roles: new List<string> { "User" },
            creationUserId: Guid.NewGuid(),
            creationUserName: "System",
            accountId: Guid.NewGuid(),
            accountName: "Default Account"
        );

        var createdUser = await _userRepository.CreateUserAsync(user);

        if (createdUser != null)
        {
            AddMessage(new ResultMessage
            {
                Code = "201",
                Text = "Usuário criado com sucesso.",
                Type = ResultMessageTypes.Success
            });

            return AuthResult.SignUpSuccess(
                createdUser.Id,
                createdUser.Name,
                "Usuário criado com sucesso"
            );
        }
        else
        {
            throw new Exception("Falha ao criar usuário no repositório");
        }
    }
    catch (Exception ex)
    {
        AddMessage(new ResultMessage
        {
            Code = "500",
            Text = $"Erro ao criar usuário: {ex.Message}",
            Type = ResultMessageTypes.Error
        });

        return AuthResult.Failure($"Erro interno ao criar usuário: {ex.Message}");
    }
}



































private static string GenerateUserNameFromAccessKey(string accessKey)
{

    if (accessKey.Contains('@'))
    {

        return accessKey.Split('@')[0];
    }

    else
    {

        return $"User_{accessKey}";
    }
}

/**/