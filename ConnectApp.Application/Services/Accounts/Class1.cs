//using ConnectApp.Application.DTOs.Accounts;
//using ConnectApp.Domain.Entities.Accounts;
//using ConnectApp.Domain.Interfaces.Accounts;
//using ConnectApp.Shared.Results;

//namespace ConnectApp.Application.Services.Accounts
//{
//    public class AuthParams
//    {
//        private readonly IAccountRepository _repository;
//        public AuthParams(IAccountRepository repository)
//        { 

//            _repository = repository;
//        }


//        //        using ConnectApp.Application.DTOs.Accounts;
//        //using ConnectApp.Application.Interfaces.Accounts;
//        //using ConnectApp.Domain.Entities.Accounts;
//        //using ConnectApp.Domain.Interfaces.Accounts;
//        //using ConnectApp.Shared.Results;

//        //namespace ConnectApp.Application.Services.Accounts
//        //    {
//        //        public class AccountService : IAccountService
//        //        {
//        //            private readonly IAccountRepository _repository;

//        //            public AccountService(IAccountRepository repository)
//        //            {
//        //                _repository = repository;
//        //            }


//        //---------------CREATE-------------------

//        public async Task<Result<Account>> CreatesAccountAsync(AccountParams @params)
//        {
//            try
//            {
//                // Validação inicial dos parâmetros
//                var validationResult = ValidateParams(@params);
//                if (!validationResult.IsValid)
//                    return Result<Account>.Failure(validationResult.ErrorMessage);

//                // Criação da conta
//                var account = CreateAccountFromParams(@params);

//                // Validação da entidade (opcional, mas recomendado)
//                var entityValidation = ValidateAccountEntity(account);
//                if (!entityValidation.IsValid)
//                    return Result<Account>.Failure(entityValidation.ErrorMessage);

//                // Persistência
//                await _repository.CreateAsync(account);
//                //await _repository.SaveChangesAsync();

//                //_logger.LogInformation("Conta criada com sucesso: {Id}", account.Id);

//                return Result<Account>.Success(account, "Conta criada com sucesso.");
//            }
//            catch (Exception ex)
//            {
//                //_logger.LogError(ex, "Erro ao criar conta: {Message}", ex.Message);
//                return Result<Account>.Failure($"Erro ao criar conta: {ex.Message}");
//            }
//        }

//        private static AccountResult ValidateParams(AccountParams @params)
//        {
//            if (@params == null)
//                return ValidationResult.Invalid("Parâmetros não podem ser nulos.");

//            if (string.IsNullOrWhiteSpace(@params.Name))
//                return ValidationResult.Invalid("O nome da conta é obrigatório.");

//            if (@params.UserId == Guid.Empty)
//                return ValidationResult.Invalid("O ID do usuário é obrigatório.");

//            // Validações de negócio usando AccountSetParams
//            if (!Account.AccountSetParams.IsValidAccountName(@params.Name))
//                return ValidationResult.Invalid("Nome da conta deve ter entre 2 e 100 caracteres.");

//            if (!string.IsNullOrWhiteSpace(@params.TemaPadrao) &&
//                !Account.AccountSetParams.IsValidTema(@params.TemaPadrao))
//                return ValidationResult.Invalid($"Tema inválido. Temas permitidos: {string.Join(", ", Account.AccountSetParams.GetTemasPermitidos())}");

//            return ValidationResult.Valid();
//        }

//        private static Account CreateAccountFromParams(AccountParams @params)
//        {
//            return Account.AccountSetParams.Create(
//                accountName: @params.Name!,
//                creationUserId: @params.UserId,
//                creationUserName: @params.UserName,
//                temaPadrao: @params.TemaPadrao,
//                urlLogo: @params.UrlLogo,
//                urlIcone: @params.UrlIcone,
//                urlImagemLogin: @params.UrlImagemLogin,
//                urlImagemDashboard: @params.UrlImagemDashboard
//            );
//        }

//        private static AccountResult ValidateAccountEntity(Account account)
//        {
//            // Validações adicionais na entidade, se necessário
//            if (account.Id == Guid.Empty)
//                return ValidationResult.Invalid("ID da conta inválido.");

//            return ValidationResult.Valid();
//        }



//        //public async Task<AccountResult> CreatesAccountAsync(AccountParams @params)
//        //{
//        //    try
//        //    {
//        //        var account = CreateAccountParams(@params);

//        //        await _repository.CreateAsync(account);


//        //        return AccountResult.Success("Conta criada com sucesso.");
//        //    }
//        //    catch (ArgumentException ex)
//        //    {
//        //        return AccountResult.Failure(ex.Message);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return AccountResult.Failure($"Erro interno ao criar conta: {ex.Message}");
//        //    }
//        //}

//        //---------------UPDATE-------------------
//        public async Task<AccountResult> UpdateAccountByIdAsync(AccountParams dto)
//        {
//            try
//            {
//                var account = await _repository.GetByIdAsync(dto.Id!.Value);
//                if (account == null)
//                    return AccountResult.Failure("Conta não encontrada.");

//                await UpdateAccountService(dto, account);

//                return AccountResult.Success("Conta atualizada com sucesso.");
//            }
//            catch (ArgumentException ex)
//            {
//                return AccountResult.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return AccountResult.Failure($"Erro interno ao atualizar conta: {ex.Message}");
//            }
//        }



//        //---------------------DELETE----------------------------
//        public async Task<AccountResult> DeleteAccountAsync(AccountParams dto)
//        {
//            try
//            {
//                var account = await _repository.GetByIdAsync(dto.Id!.Value);
//                if (account == null)
//                    return AccountResult.Failure("Conta não encontrada.");

//                account.Deactivate(dto.UserId, dto.UserName ?? "Sistema");

//                await _repository.UpdateAsync(account);

//                return AccountResult.Success("Conta desativada com sucesso.");
//            }
//            catch (ArgumentException ex)
//            {
//                return AccountResult.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return AccountResult.Failure($"Erro interno ao desativar conta: {ex.Message}");
//            }
//        }


//        //----------------------READ-----------------------------
//        public async Task<AccountResult> GetAccountByIdAsync(Guid id)
//        {
//            try
//            {
//                var account = await _repository.GetByIdAsync(id);
//                return new AccountResult
//                {
//                    AccountId = account.Id,
//                    AccountName = account.Name,
//                    Ativa = account.Ativa,
//                    Error = false,
//                    Message = "Conta localizada com sucesso."


//                };

//            }
//            catch (Exception ex)
//            {
//                new AccountResult
//                {
//                    Error = true,
//                    Message = $"Erro Interno: {ex.Message}",
//                    AccountId = id,


//                };
//                return AccountResult.Failure("Erro ao localizar conta.");
//            }
//        }

//        public async Task<IList<AccountResult>> GetAllAccountsAsync()
//        {
//            try
//            {
//                var accounts = await _repository.GetAllAsync();


//                return AccountMapper(accounts);

//            }
//            catch (Exception ex)
//            {
//                new ResultMessage
//                {
//                    Code = "500",
//                    Text = $"Erro Interno: {ex.Message}",
//                    Type = ResultMessageTypes.Error
//                };
//                return [];
//            }
//        }



//        //--------------- Métodos Auxiliares --------------------
//        private static IList<AccountResult> AccountMapper(IList<Account> accounts)
//        {
//            var results = new List<AccountResult>();

//            foreach (var account in accounts)
//            {
//                var accountResult = new AccountResult
//                {
//                    // Corrigido: Atribuindo valores às propriedades
//                    AccountId = account.Id,
//                    AccountName = account.Name,
//                    Ativa = account.Ativa,
//                    //TemaPadrao = account.TemaPadrao,
//                    //UrlLogo = account.UrlLogo,
//                    //UrlIcone = account.UrlIcone,
//                    //UrlImagemLogin = account.UrlImagemLogin,
//                    //UrlImagemDashboard = account.UrlImagemDashboard,
//                    //CreationDate = account.CreationDate,
//                    //CreationUserId = account.CreationUserId,
//                    //CreationUserName = account.CreationUserName,
//                    //ChangeDate = account.ChangeDate,
//                    //ChangeUserId = account.ChangeUserId,
//                    //ChangeUserName = account.ChangeUserName,
//                    //ExclusionDate = account.ExclusionDate,
//                    //ExclusionUserId = account.ExclusionUserId,
//                    //ExclusionUserName = account.ExclusionUserName,
//                    //RecordStatus = account.RecordStatus
//                };
//                results.Add(accountResult);
//            }
//            return results;
//        }
//        //private static Account CreateAccountParams(AccountParams @params)
//        //{
//        //    return Account.Create(
//        //        @params.Name,
//        //        @params.UserId,
//        //        @params.UserName!,
//        //        @params.TemaPadrao,
//        //        @params.UrlLogo,
//        //        @params.UrlIcone,
//        //        @params.UrlImagemLogin,
//        //        @params.UrlImagemDashboard
//        //    );
//        //}
//        private async Task UpdateAccountService(AccountParams dto, Account account)
//        {
//            account.Update(
//                                dto.Name,
//                                dto.TemaPadrao,
//                                dto.UrlLogo,
//                                dto.UrlIcone,
//                                dto.UrlImagemLogin,
//                                dto.UrlImagemDashboard,
//                                dto.UserId,
//                                dto.UserName);

//            await _repository.UpdateAsync(account);
//        }


//    }
//}



