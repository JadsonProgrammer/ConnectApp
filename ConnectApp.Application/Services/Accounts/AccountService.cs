using ConnectApp.Application.DTOs.Accounts;
using ConnectApp.Application.Interfaces.Accounts;
using ConnectApp.Domain.Entities.Accounts;
using ConnectApp.Domain.Interfaces.Accounts;

namespace ConnectApp.Application.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<AccountResult> CreateAccountAsync(AccountParams dto)
        {
            try
            {
                var account = Account.Create(
                    dto.AccountName,
                    dto.UserId,
                    dto.UserName,
                    dto.TemaPadrao,
                    dto.UrlLogo,
                    dto.UrlIcone,
                    dto.UrlImagemLogin,
                    dto.UrlImagemDashboard);

                await _repository.AddAsync(account);

                return AccountResult.Success("Conta criada com sucesso.", account);
            }
            catch (ArgumentException ex)
            {
                return AccountResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return AccountResult.Failure($"Erro interno ao criar conta: {ex.Message}");
            }
        }

        public async Task<AccountResult> UpdateAccountAsync(AccountParams dto)
        {
            try
            {
                var account = await _repository.GetByIdAsync(dto.AccountId!.Value);
                if (account == null)
                    return AccountResult.Failure("Conta não encontrada.");

                account.Update(
                    dto.AccountName,
                    dto.TemaPadrao,
                    dto.UrlLogo,
                    dto.UrlIcone,
                    dto.UrlImagemLogin,
                    dto.UrlImagemDashboard,
                    dto.UserId,
                    dto.UserName);

                await _repository.UpdateAsync(account);

                return AccountResult.Success("Conta atualizada com sucesso.", account);
            }
            catch (ArgumentException ex)
            {
                return AccountResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return AccountResult.Failure($"Erro interno ao atualizar conta: {ex.Message}");
            }
        }

        public async Task<AccountResult> DeleteAccountAsync(AccountParams dto)
        {
            try
            {
                var account = await _repository.GetByIdAsync(dto.AccountId!.Value);
                if (account == null)
                    return AccountResult.Failure("Conta não encontrada.");

                account.Deactivate(dto.UserId, dto.UserName ?? "Sistema");

                await _repository.UpdateAsync(account);

                return AccountResult.Success("Conta desativada com sucesso.", account);
            }
            catch (ArgumentException ex)
            {
                return AccountResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return AccountResult.Failure($"Erro interno ao desativar conta: {ex.Message}");
            }
        }
    }
}
