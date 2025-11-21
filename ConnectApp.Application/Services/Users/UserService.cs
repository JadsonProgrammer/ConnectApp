    using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace ConnectApp.Application.Services.Users
{
    public class UserService : ResultService2, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IJwtTokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        //-----------------------CREATE USER-----------------------//
        public async Task<Result<UserResult>> CreatesUserByAccountAsync(UserParams userParams)
        {
            var userId = _httpContextAccessor.GetUserId();
            var userName = _httpContextAccessor.GetUserName();
            var accountId = _httpContextAccessor.GetAccountId();
            var accountName = _httpContextAccessor.GetAccountName();

            try
            {
                var user = User.Create(
                    name: userParams.Name,
                    cpf: userParams.CPF!,
                    accessKey: userParams.AccessKey,
                    password: _tokenService.HashPassword(userParams.Password),
                    phones: userParams.Phones,
                    addresses: userParams.Addresses,
                    emails: userParams.Emails,
                    roles: userParams.Roles,
                    creationUserId: userId,
                    creationUserName: userName,
                    accountId: accountId,
                    accountName: accountName
                );

                var createdUser = await _userRepository.CreateUserAsync(user);

                var dto = new UserResult { Id = createdUser.Id, Nome = createdUser.Name };
                return Success(dto, ""); // mensagem em branco => BaseController irá gerar padrão
            }
            catch (Exception ex)
            {
                return Failure<UserResult>("Erro ao criar usuário", ex.Message);
            }


        }
        public async Task<Result<UserResult>> CreatesUserAsync(UserParams userParams)
        {
            var userId = _httpContextAccessor.GetUserId();
            var userName = _httpContextAccessor.GetUserName();
            var accountId = _httpContextAccessor.GetAccountId();
            var accountName = _httpContextAccessor.GetAccountName();

            try
            {
                var user = User.Create(
                    name: userParams.Name,
                    cpf: userParams.CPF!,
                    accessKey: userParams.AccessKey,
                    password: _tokenService.HashPassword(userParams.Password),
                    phones: userParams.Phones,
                    addresses: userParams.Addresses,
                    emails: userParams.Emails,
                    roles: userParams.Roles,
                    creationUserId: userId,
                    creationUserName: userName,
                    accountId: accountId,
                    accountName: accountName
                );

                var createdUser = await _userRepository.CreateUserAsync(user);

                var dto = new UserResult { Id = createdUser.Id, Nome = createdUser.Name };
                return Success(dto, ""); // mensagem em branco => BaseController irá gerar padrão
            }
            catch (Exception ex)
            {
                return Failure<UserResult>("Erro ao criar usuário", ex.Message);
            }

        }

        //-----------------------GET USERS-----------------------//
        public async Task<Result<IList<UserResult>>> GetAllusersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUserAsync();
                var dtos = users.Select(u => new UserResult { Id = u.Id, Nome = u.Name }).ToList();
                return Success<IList<UserResult>>(dtos, "");
            }
            catch (Exception ex)
            {
                return Failure<IList<UserResult>>("Erro ao listar usuários", ex.Message);
            }
        }

        public async Task<Result<UserResult>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null) return Failure<UserResult>("Usuário não encontrado");
                return Success(new UserResult { Id = user.Id, Nome = user.Name }, "");
            }
            catch (Exception ex)
            {
                return Failure<UserResult>("Erro ao buscar usuário", ex.Message);
            }
        }

        //-----------------------UPDATE USER-----------------------//
        
        public async Task<Result<UserResult>> UpdateUserByIdAsync(UserParams userParams, Guid id )
        {
            try
            {
                var existing = await _userRepository.GetUserByIdAsync((Guid)id!);
                if (existing == null) return Failure<UserResult>("Usuário não encontrado");

                existing.Update(
                    name: userParams.Name,
                    cpf: userParams.CPF,
                    phones: userParams.Phones,
                    addresses: userParams.Addresses,
                    emails: userParams.Emails,
                    avatar: userParams.Avatar,
                    note: userParams.Note,
                    changeUserId: _httpContextAccessor.GetUserId(),
                    changeUserName: _httpContextAccessor.GetUserName()
                );

                if (!string.IsNullOrWhiteSpace(userParams.Password))
                    existing.Password = _tokenService.HashPassword(userParams.Password);

                var updated = await _userRepository.UpdateUserAsync(existing);
                return Success(new UserResult { Id = updated!.Id, Nome = updated.Name }, "");
            }
            catch (Exception ex)
            {
                return Failure<UserResult>("Erro ao atualizar usuário", ex.Message);
            }
        }

        //-----------------------DELETE USER-----------------------//
        public async Task<Result<bool>> DeleteUserByIdAsync(Guid id)
        {
            
            try
            {
                var existing = await _userRepository.GetUserByIdAsync(id);
                if (existing == null) return Failure<bool>("Usuário não encontrado");

                existing.Deactivate(_httpContextAccessor.GetUserId(), _httpContextAccessor.GetUserName());
                await _userRepository.UpdateUserAsync(existing);
                return Success(true, "");
            }
            catch (Exception ex)
            {
                return Failure<bool>("Erro ao excluir usuário", ex.Message);
            }
        }



        //-----------------------CREDENTIAL USER-----------------------//
        public async Task<Credentials?> GetCredentialByIdAsync(Guid userId)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        
    }
}