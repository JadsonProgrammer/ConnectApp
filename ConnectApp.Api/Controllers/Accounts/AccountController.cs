using ConnectApp.Application.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Accounts
{
    [Route("sign-up")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService _accountService)
        {
            this._accountService = _accountService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            try
            {
                // Service já retorna DTO (não Entity)
                var accountDto = await _accountService.GetAccountByIdAsync(id);

                if (accountDto == null)
                {
                    return NotFound(new ResponseMessage
                    {
                        Code = "404",
                        Text = "Account não encontrada"
                    });
                }

                return Ok(accountDto); // ✅ JSON seguro para frontend
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessage
                {
                    Code = "500",
                    Text = $"Erro Interno: {e.Message}"
                });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                // Service retorna lista de DTOs
                var accounts = await _accountService.GetAllAccountsAsync();

                if (accounts == null || accounts.Count == 0)
                {
                    return NotFound(new ResponseMessage
                    {
                        Code = "404",
                        Text = "Nenhuma account encontrada"
                    });
                }

                return Ok(accounts); // ✅ Lista de JSONs seguros
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessage
                {
                    Code = "500",
                    Text = $"Erro Interno: {e.Message}"
                });
            }
        }


        //[HttpGet("list")]
        //public async Task<IActionResult> GetActionResultAsync()
        //{
        //    try
        //    {
        //        var accounts = await _accountService.GetAllAsync();

        //        if (accounts == null || accounts.Count == 0)
        //        {
        //            return NotFound(new ResponseMessage
        //            {
        //                Code = "404",
        //                Text = "Nenhum Usuário encontrado!"
        //            });
        //        }
        //        var accountsDtos = AccountMapper.MapToList(accounts);


        //        return Ok(accountsDtos);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new ResponseMessage
        //        {
        //            Code = "404",
        //            Text = $"Erro Interno {e}"
        //        });
        //    }
        //}

        //[HttpGet("{id}/edit")]
        //public async Task<IActionResult> GetActionResultAsync(AccountParams account)
        //{
        //    try
        //    {
        //        var accounts = await _accountService.GetAccountByIdAsync(account);

        //        if (accounts == null)
        //        {
        //            return NotFound(new ResponseMessage
        //            {
        //                Code = "404",
        //                Text = "Nenhum Usuário encontrado!"
        //            });
        //        }
        //        //var accountDtos = UserMapper.MapToSearch(accounts);                              


        //        return Ok(accounts);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new ResponseMessage
        //        {
        //            Code = "404",
        //            Text = $"Erro Interno {e}"
        //        });
        //    }
        //}


        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateActionResultAsync([FromBody] AccountParams account)
        {
            try
            {
                var accounts = await _accountService.UpdateAccountByIdAsync(account);


                //var accountDtos = UserMapper.MapToSearch(accounts);                              

                if (accounts == false)
                {
                    return NotFound(new ResponseMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário Alterado!"
                    });

                }
                return Ok(new ResponseMessage
                {
                    Code = "202",
                    Text = "Conta alterada com sucesso",
                    Id = account.AccountId,
                    Status = accounts
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessage
                {
                    Code = "404",
                    Text = $"Erro Interno {e}"
                });
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountParams account)
        {
            try
            {
                var result = await _accountService.CreateAccountAsync(account);

                if (result == null)
                {
                    return NotFound(new ResponseMessage
                    {
                        Code = "404",
                        Text = "Erro ao criar account!"
                    });
                }

                // ✅ Verifica se houve erro no service
                if (result.Error)
                {
                    return BadRequest(new ResponseMessage
                    {
                        Code = "400",
                        Text = result.Message ?? "Erro ao criar account!"
                    });
                }

                // ✅ Busca a account criada para retornar os dados completos
                //var accountCriada = await _accountService.GetAccountByIdAsync(account.AccountId);

                //if (accountCriada == null)
                //{
                //    return Ok(new ResponseMessage
                //    {
                //        Code = "201",
                //        Text = "Account criada com sucesso, mas não foi possível retornar os dados."
                //    });
                //}

                // ✅ Usa o mapper para converter Entity → DTO
                // var accountDto = AccountMapper.MapToParams(accountCriada);

                return Ok(result); // ✅ Retorna DTO mapeado
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessage
                {
                    Code = "500",
                    Text = $"Erro Interno: {e.Message}"
                });
            }
        }



























































        //    [HttpPost("{id}/create")]
        //    public async Task<IActionResult> CreateActionResultAsync([FromBody] Account account)
        //    {
        //        try
        //        {
        //            var accounts = await _accountService.CreateAccountAsync(account);

        //            if (accounts == null)
        //            {
        //                return NotFound(new ResponseMessage
        //                {
        //                    Code = "404",
        //                    Text = "Nenhum Usuário selecionado!"
        //                });
        //            }
        //            var accountDtos = AccountMapper.MapToSearch(accounts);                              

        //            if (accounts.Erro == true)
        //            {
        //                return NotFound(new ResponseMessage
        //                {
        //                    Code = "404",
        //                    Text = "Nenhum Usuário Alterado!"
        //                });

        //            }
        //            return Ok(accounts);
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest(new ResponseMessage
        //            {
        //                Code = "404",
        //                Text = $"Erro Interno {e}"
        //            });
        //        }
        //    }      
        //}
    }
}
