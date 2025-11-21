using ConnectApp.Application.DTOs.Accounts;
using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Accounts;
using ConnectApp.Domain.Entities.Accounts;
using ConnectApp.Domain.Interfaces.Accounts;
using ConnectApp.Shared.Results;

namespace ConnectApp.Application.Services.Accounts
{
    public class AccountService : ResultService2, IAccountService
    {
        private readonly IAccountRepository _repository;
        // private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<AccountResult>> CreatesAccountAsync(AccountParams @params)
        {
            try
            {
                var validationResult = ValidateParams(@params);
                if (!validationResult.IsValid)
                    return Result<AccountResult>.Failure(validationResult.ErrorMessage);

                var account = CreateAccountFromParams(@params);
                await _repository.CreateAsync(account);
                //await _repository.SaveChangesAsync();
                var dto = new AccountResult
                {
                    Successo = true,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Ativa = account.Ativa,
                    TemaPadrao = account.TemaPadrao,
                    UrlLogo = account.UrlLogo,
                    UrlIcone = account.UrlIcone,
                    UrlImagemLogin = account.UrlImagemLogin,
                    UrlImagemDashboard = account.UrlImagemDashboard,
                    CreationDate = account.CreationDate,
                    CreationUserId = account.CreationUserId,
                    CreationUserName = account.CreationUserName
                };
                return Success(dto, "");
            }
            catch (ArgumentException ex)
            {
                return Failure<AccountResult>("Erro ao criar a Conta", ex.Message);
            }
            catch (Exception ex)
            {
                return Failure<AccountResult>($"Erro interno ao criar conta: {ex.Message}");
            }
        }


        public async Task<Result<IList<AccountResult>>> GetAllAccountsAsync()
        {
            try
            {
                var accounts = await _repository.GetAllAsync();
                var dtos = accounts.Select(account => new AccountResult
                {
                    Successo = true,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Ativa = account.Ativa,
                    TemaPadrao = account.TemaPadrao,
                    UrlLogo = account.UrlLogo,
                    UrlIcone = account.UrlIcone,
                    UrlImagemLogin = account.UrlImagemLogin,
                    UrlImagemDashboard = account.UrlImagemDashboard,
                    CreationDate = account.CreationDate,
                    CreationUserId = account.CreationUserId,
                    CreationUserName = account.CreationUserName,
                    ChangeDate = account.ChangeDate,
                    ChangeUserId = account.ChangeUserId,
                    ChangeUserName = account.ChangeUserName,
                    Message = "Conta localizada com sucesso."
                }).ToList();
                return Success<IList<AccountResult>>(dtos, "");
            }
            catch (Exception ex)
            {
                return Failure<IList<AccountResult>>("Erro ao listar Contas", ex.Message);
            }
        }

        public async Task<Result<AccountResult>> GetAccountByIdAsync(Guid id)
        {
            try
            {
                var account = await _repository.GetByIdAsync(id);
                if (account == null) return Failure<AccountResult>("Conta não encontrada.");

                return Success(new AccountResult
                {
                    Successo = true,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Ativa = account.Ativa,
                    TemaPadrao = account.TemaPadrao,
                    UrlLogo = account.UrlLogo,
                    UrlIcone = account.UrlIcone,
                    UrlImagemLogin = account.UrlImagemLogin,
                    UrlImagemDashboard = account.UrlImagemDashboard,
                    Message = "Conta localizada com sucesso."
                });

            }
            catch (Exception ex)
            {
                return Failure<AccountResult>($"Erro interno ao buscar conta: {ex.Message}");
            }
        }

        public async Task<Result<AccountResult>> UpdateAccountByIdAsync(AccountParams accountParams, Guid id)
        {
            try
            {
                var account = await _repository.GetByIdAsync(id);
                if (account == null) return Failure<AccountResult>("Conta não encontrada.");

                var validationResult = ValidateParams(accountParams);
                if (!validationResult.IsValid)
                    return Failure<AccountResult>(validationResult.ErrorMessage);

                if (accountParams.AccountId == null)
                    return Failure<AccountResult>("ID da conta é obrigatório.");

                var updated = await _repository.UpdateAsync(account, id);

                if (updated == false)
                {
                    return Failure<AccountResult>("Erro ao atualizar a conta.");
                }
                return Success(new AccountResult
                {
                    Successo = true,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Ativa = account.Ativa,
                    TemaPadrao = account.TemaPadrao,
                    UrlLogo = account.UrlLogo,
                    UrlIcone = account.UrlIcone,
                    UrlImagemLogin = account.UrlImagemLogin,
                    UrlImagemDashboard = account.UrlImagemDashboard,
                    Message = "Conta atualizada com sucesso."
                });



            }
            catch (ArgumentException ex)
            {
                return Failure<AccountResult>($"Erro ao criar a conta! {ex.Message}");
            }
            catch (Exception ex)
            {
                return Failure<AccountResult>($"Erro interno ao atualizar conta: {ex.Message}");
            }
        }

        public async Task<Result<AccountResult>> DeleteAccountByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Failure<AccountResult>($"ID da conta é obrigatório.");

                }
                var account = await _repository.GetByIdAsync(id);
                if (account == null)
                    return Failure<AccountResult>("Conta não encontrada.");

                var deleted = await _repository.DeleteAsync(id);
                if (deleted == false)
                {
                    return Failure<AccountResult>("Erro ao desativar a conta.");
                }
                return Success(new AccountResult
                {
                    Successo = true,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Ativa = account.Ativa,
                    TemaPadrao = account.TemaPadrao,
                    UrlLogo = account.UrlLogo,
                    UrlIcone = account.UrlIcone,
                    UrlImagemLogin = account.UrlImagemLogin,
                    UrlImagemDashboard = account.UrlImagemDashboard,
                    Message = "Conta desativada com sucesso."
                });



            }
            catch (Exception ex)
            {
                return Failure<AccountResult>($"Erro interno ao desativar conta: {ex.Message}");
            }
        }


        //public async Task<AccountResult> DeleteAccountAsync2(AccountParams dto)
        //{
        //    try
        //    {
        //        if (dto.Id == null || dto.Id == Guid.Empty)
        //            return AccountResult.Failure("ID da conta é obrigatório.");

        //        if (dto.UserId == Guid.Empty)
        //            return AccountResult.Failure("ID do usuário é obrigatório.");

        //        var account = await _repository.GetByIdAsync(dto.Id.Value);
        //        if (account == null)
        //            return AccountResult.Failure("Conta não encontrada.");

        //        // Usando o método Deactivate do AccountSetParams
        //        Account.AccountSetParams.Deactivate(account, dto.UserId, dto.UserName);
        //        await _repository.UpdateAsync(account);

        //        return AccountResult.Success("Conta desativada com sucesso.");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return AccountResult.Failure(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return AccountResult.Failure($"Erro interno ao desativar conta: {ex.Message}");
        //    }
        //}



        private static IList<AccountResult> AccountMapper(IList<Account> accounts)
        {
            return [.. accounts.Select(account => new AccountResult
            {
                Successo = true,
                AccountId = account.Id,
                AccountName = account.Name,
                Ativa = account.Ativa,
                TemaPadrao = account.TemaPadrao,
                UrlLogo = account.UrlLogo,
                UrlIcone = account.UrlIcone,
                UrlImagemLogin = account.UrlImagemLogin,
                UrlImagemDashboard = account.UrlImagemDashboard,
                CreationDate = account.CreationDate,
                CreationUserId = account.CreationUserId,
                CreationUserName = account.CreationUserName,
                ChangeDate = account.ChangeDate,
                ChangeUserId = account.ChangeUserId,
                ChangeUserName = account.ChangeUserName,
                Message = "Conta localizada com sucesso."
            })];
        }

        private static ValidationResult ValidateParams(AccountParams @params)
        {
            if (@params == null)
                return ValidationResult.Invalid("Parâmetros não podem ser nulos.");

            if (string.IsNullOrWhiteSpace(@params.AccountName))
                return ValidationResult.Invalid("O nome da conta é obrigatório.");

            if (@params.UserId == Guid.Empty)
                return ValidationResult.Invalid("O ID do usuário é obrigatório.");

            if (!Account.AccountSetParams.IsValidAccountName(@params.AccountName))
                return ValidationResult.Invalid("Nome da conta deve ter entre 2 e 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(@params.TemaPadrao) &&
                !Account.AccountSetParams.IsValidTema(@params.TemaPadrao))
                return ValidationResult.Invalid($"Tema inválido. Temas permitidos: {string.Join(", ", Account.AccountSetParams.GetTemasPermitidos())}");

            return ValidationResult.Valid();
        }

        private static Account CreateAccountFromParams(AccountParams @params)
        {
            return Account.AccountSetParams.Create(
                accountName: @params.AccountName!,
                creationUserId: @params.UserId,
                creationUserName: @params.UserName,
                temaPadrao: @params.TemaPadrao,
                urlLogo: @params.UrlLogo,
                urlIcone: @params.UrlIcone,
                urlImagemLogin: @params.UrlImagemLogin,
                urlImagemDashboard: @params.UrlImagemDashboard
            );
        }

        private async Task UpdateAccountService(AccountParams dto, Account account)
        {
            Account.AccountSetParams.Update(
                account: account,
                accountName: dto.AccountName,
                temaPadrao: dto.TemaPadrao,
                urlLogo: dto.UrlLogo,
                urlIcone: dto.UrlIcone,
                urlImagemLogin: dto.UrlImagemLogin,
                urlImagemDashboard: dto.UrlImagemDashboard,
                changeUserId: dto.UserId,
                changeUserName: dto.UserName
            );

            await _repository.UpdateAsync(account,(Guid)account.Id);
        }

       

        private class ValidationResult
        {
            public bool IsValid { get; }
            public string ErrorMessage { get; }

            public ValidationResult(bool isValid, string errorMessage = "")
            {
                IsValid = isValid;
                ErrorMessage = errorMessage;
            }

            public static ValidationResult Valid() => new(true);
            public static ValidationResult Invalid(string errorMessage) => new(false, errorMessage);
        }

    }
}