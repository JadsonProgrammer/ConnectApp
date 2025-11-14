using ConnectApp.Api.Controllers.Base;
using ConnectApp.Application.DTOs.Auths;
using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Auths
{
    [Route("auth/")]
    public class AuthController : BaseController
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
                    return await CreateGetResponse(result);
                }
                catch (Exception e)
                {
                    return await CreateExceptionResponse(e);
                }



                //var result = await _authService.SignInAsync(authParams);

                //if (_authService.HasErrors())
                //{
                //    var error = new ResultMessage
                //    {
                //        Code = _authService.Errors[0].Code,
                //        Text = _authService.Errors[0].Text
                //    };
                //    return Unauthorized(error);
                //}
                //else if (result is null)
                //{
                //    return BadRequest(new ResultMessage
                //    {
                //        Code = "400",
                //        Text = "Usuário ou senha inválidos"
                //    });
                //}

                //return Ok(result);
           
        }


        [HttpPost("check-login")]
        public async Task<IActionResult> CheckLogin([FromBody] AuthCheck request)
        {


            try
            {
                var exists = await _authService.LoginExistsAsync(request);
                return await CreateGetResponse(exists);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }

            //if (string.IsNullOrWhiteSpace(request.AccessKey))
            //    return BadRequest("Login não informado.");


            

            //return Ok(!exists); 
        }


        //[HttpPost("sign-up")]
        //[AllowAnonymous]
        //public async Task<IActionResult> SignUpAsync([FromBody] AuthCheck authParams)
        //{

        //    try
        //    {
        //        var result = await _authService.SignUpAsync(authParams);
        //        return await CreatePostResponse(result, _authService);
        //    }
        //    catch (Exception e)
        //    {
        //        return await CreateExceptionResponse(e);
        //    }


        //    try
        //    {
        //        var result = await _authService.SignUpAsync(authParams);

        //        if (result.Error)
        //            return BadRequest(result);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new AuthResult
        //        {
        //            Error = true,
        //            Message = $"Erro interno: {ex.Message}"
        //        });
        //    }
        //}


    }
}