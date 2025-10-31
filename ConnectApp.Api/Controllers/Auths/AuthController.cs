using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Auths
{
    [Route("auth/")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }


        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignInAsync([FromBody] AuthParams authParams)
        {
            try
            {
                var result = await _authService.SignInAsync(authParams);

                if (_authService.HasErrors())
                {
                    var error = new ResultMessage
                    {
                        Code = _authService.Errors[0].Code,
                        Text = _authService.Errors[0].Text
                    };
                    return Unauthorized(error);
                }
                else if (result is null)
                {
                    return BadRequest(new ResultMessage
                    {
                        Code = "400",
                        Text = "Usuário ou senha inválidos"
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultMessage
                {
                    Code = "500",
                    Text = $"Erro: {ex.Message}"
                });
            }
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAsync([FromBody] User user)
        {

            try
            {
                var result = await _authService.SignUpAsync(user);

                if (result.Error == false)
                {
                    var resultMessage = new ResultMessage
                    {
                        Code = "200",
                        Text = "Login disponível"
                    };
                    return Ok(resultMessage);
                }
                else
                {
                    return BadRequest(new ResultMessage
                    {
                        Code = "400",
                        Text = result.Message ?? "Não foi possível realizar o cadastro"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultMessage
                {
                    Code = "500",
                    Text = $"Erro: {ex.Message}"
                });
            }
        }


        [HttpPost("check-login")]
        public async Task<IActionResult> CheckLogin([FromBody] AuthParams request)
        {
            if (string.IsNullOrWhiteSpace(request.AccessKey))
                return BadRequest("Login não informado.");


            var exists = await _authService.LoginExistsAsync(request);

            return Ok(!exists); // true = disponível, false = já existe
        }



    }
}