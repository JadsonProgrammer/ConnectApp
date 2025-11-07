using ConnectApp.Api.Controllers.Base;
using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Users
{
    [Authorize]
    [Route("users")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var userResult = await _userService.GetUserByIdAsync(id);
                return await CreateGetResponse(userResult, _userService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllusersAsync();
                return await CreateGetResponse(users, _userService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] UserParams userParams , Guid id)
        {
            try
            {
                var result = await _userService.UpdateUserByIdAsync(userParams);
                return await CreateGetResponse(result, _userService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _userService.DeleteUserByIdAsync(id);
                return await CreateGetResponse(result, _userService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserParams userParams)
        {
            try
            {
                var result = await _userService.CreatesUserAsync(userParams);
                return await CreatePostResponse(result, _userService);
            }
            catch (Exception e)
            {
                return await CreateExceptionResponse(e);
            }
        }
    }
}
