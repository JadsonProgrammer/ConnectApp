using Microsoft.AspNetCore.Mvc;
using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Shared.Results;
using ConnectApp.Api.Controllers.Base;

namespace ConnectApp.Api.Controllers.Users
{
    
    [Route("users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserParams userParams)
        {
            var result = await _userService.CreatesUserAsync(userParams);
            return await CreatePostResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return await CreateGetResponse(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAllusersAsync();
                return await CreateGetResponse(result);
            }
            catch (Exception ex)
            {

                return await CreateExceptionResponse(ex);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserById([FromBody] UserParams userParams, Guid id)
        {
            try
            {
                var result = await _userService.UpdateUserByIdAsync(userParams, id);
                return await CreateGetResponse(result);
            }
            catch (Exception ex)
            {
                return await CreateExceptionResponse(ex);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUserByIdAsync(id);
                return await CreateGetResponse(result);
            }
            catch (Exception ex)
            {
                return await CreateExceptionResponse(ex);
            }
        }
    }
}
