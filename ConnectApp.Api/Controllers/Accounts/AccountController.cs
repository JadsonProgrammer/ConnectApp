using ConnectApp.Api.Controllers.Base;
using ConnectApp.Application.DTOs.Accounts;
using ConnectApp.Application.Interfaces.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Accounts
{
    [Route("sign-up")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            try
            {
                var accountResult = await _accountService.GetAccountByIdAsync(id);
                return await CreateGetResponse(accountResult);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accountResult = await _accountService.GetAllAccountsAsync();
                return await CreateGetResponse(accountResult);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] AccountParams account, Guid id)
        {
            try
            {
                var accountResult = await _accountService.UpdateAccountByIdAsync(account, id);
                return await    CreateGetResponse(accountResult);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountParams account)
        {
            try
            {
                var accountResult = await _accountService.CreatesAccountAsync(account);
                return await CreatePostResponse(accountResult);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountById(Guid id)
        {
            try
            {
                var accountResult = await _accountService.DeleteAccountByIdAsync(id);
                return await CreateGetResponse(accountResult);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }
    }
}
/*
using ConnectApp.Api.Controllers.Base;
using ConnectApp.Application.DTOs.Accounts;
using ConnectApp.Application.Interfaces.Accounts;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Accounts
{
    [Route("sign-up")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            try
            {
                var accountResult = await _accountService.GetAccountByIdAsync(id);
                return await CreateGetResponse(accountResult, _accountService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var accountResult = await _accountService.GetAllAccountsAsync();
                return await CreateGetResponse(accountResult, _accountService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateAsync([FromBody] AccountParams account)
        {
            try
            {
                var accountResult = await _accountService.UpdateAccountByIdAsync(account);
                return await CreateGetResponse(accountResult, _accountService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatesAccountAsync([FromBody] AccountParams account)
        {
            try
            {
                var accountResult = await _accountService.CreatesAccountAsync(account);
                return await CreatePostResponse(accountResult, _accountService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }
    }
}*/