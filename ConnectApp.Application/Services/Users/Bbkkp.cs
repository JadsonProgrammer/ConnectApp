using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectApp.Application.Services.Users
{
    internal class Bbkkp
    {
        /*using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Shared.Credential;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Http;

namespace ConnectApp.Application.Services.Users
{
    public class UserService : ResultService, IUserService
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
        public async Task<UserResult> CreatesUserAsync(UserParams userParams)
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

                return new UserResult
                {
                    Id = createdUser.Id,
                    Nome = createdUser.Name,
                    Erro = false,
                    Message = "Usuário criado com sucesso"
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

                return new UserResult
                {
                    Id = CreateUserAsync(userParams).Result.Id,
                    Nome = CreateUserAsync(userParams).Result.Nome,
                    Erro = false,
                    Message = ex.Message
                };
            }
        }




        //-----------------------GET USERS-----------------------//
        public async Task<IList<UserResult?>> GetAllusersAsync()
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
        public async Task<IList<UserResult?>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var results = new List<UserResult?>();
                var users = await this._userRepository.GetUserByIdAsync(id);
                return results.Add(users);
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



        //-----------------------UPDATE USER-----------------------//

        public async Task<UserResult> UpdateUserByIdAsync(UserParams userParams, Guid? id = null)
        {

            var changeUserId = _httpContextAccessor.GetUserId();
            var changeUserName = _httpContextAccessor.GetUserName();

            try
            {
                // Buscar o usuário existente
                var userId = id ?? userParams.Id;
                var existingUser = await _userRepository.GetUserByIdAsync((Guid)userId);

                if (existingUser == null)
                {
                    throw new ArgumentException("Usuário não encontrado");
                }

                // Atualizar o usuário existente
                existingUser.Update(
                    name: userParams.Name,
                    cpf: userParams.CPF,
                    phones: userParams.Phones,
                    addresses: userParams.Addresses,
                    emails: userParams.Emails,
                    avatar: userParams.Avatar,
                    note: userParams.Note,
                    changeUserId: changeUserId,
                    changeUserName: changeUserName
                );

                // Se a senha foi fornecida, atualizar também
                if (!string.IsNullOrWhiteSpace(userParams.Password))
                {
                    existingUser.Password = _tokenService.HashPassword(userParams.Password);
                }

                // Atualizar no repositório
                var updatedUser = await _userRepository.UpdateUserAsync(existingUser);

                return new UserResult
                {
                    Id = updatedUser.Id,
                    Nome = updatedUser.Name,
                    Erro = false,
                    Message = "Usuário atualizado com sucesso"
                };
            }
            catch (Exception ex)
            {
                AddMessage(new ResultMessage
                {
                    Code = "400",
                    Text = "Erro ao atualizar usuário",
                    Type = ResultMessageTypes.Error
                });

                return new UserResult
                {
                    Id = Guid.Empty,
                    Nome = string.Empty,
                    Erro = true,
                    Message = ex.Message
                };
            }
        }

        /*
        try
        {
            //mapeando os dados do userParams para a entidade User
            var users = await this._userRepository.UpdateUserAsync(userParams);
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





        //-----------------------DELETE USER-----------------------//


        public async Task<Result<bool>> DeleteUserByIdAsync(Guid userId)
        {
            var currentUserId = _httpContextAccessor.GetUserId();
            var currentUserName = _httpContextAccessor.GetUserName();

            try
            {

                var existingUser = await _userRepository.GetUserByIdAsync(userId);
                if (existingUser == null)
                {
                    return Result<bool>.Failure("Usuário não encontrado");
                }


                if (!existingUser.RecordStatus)
                {
                    return Result<bool>.Failure("Usuário já está excluído");
                }


                existingUser.Deactivate(currentUserId, currentUserName);


                var success = await _userRepository.UpdateUserAsync(existingUser);

                if (success is null)
                {
                    return Result<bool>.Failure(false, "Falha ao excluir usuário");
                }

                return Result<bool>.Success(true, "Usuário excluído com sucesso");
            }
            catch (Exception ex)
            {

                return Result<bool>.Failure(false, $"Erro interno: {ex.Message}");
            }
        }



        //-----------------------CREDENTIAL USER-----------------------//
        public async Task<Credentials?> GetCredentialByIdAsync(Guid userId)
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






    */

    }
}
