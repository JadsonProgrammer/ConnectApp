using ConnectApp.Application.DTOs.Users;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Domain.Entities.Users;
using ConnectApp.Shared.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConnectApp.Api.Controllers.Users
{

    [Authorize]
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetActionResultAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                if (users == null || users.Count == 0)
                {
                    return NotFound(new ResultMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário encontrado!"
                    });
                }
                var userDtos = UserMapper.MapToList(users);


                return Ok(userDtos);
            }
            catch (Exception e)
            {
                return BadRequest(new ResultMessage
                {
                    Code = "404",
                    Text = $"Erro Interno {e}"
                });
            }
        }

        [HttpGet("{id}/edit")]
        public async Task<IActionResult> GetActionResultAsync(UserParams userParams)
        {
            try
            {
                var users = await _userService.GetUserByIdAsync(userParams);

                if (users == null)
                {
                    return NotFound(new ResultMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário encontrado!"
                    });
                }
                //var userDtos = UserMapper.MapToSearch(users);                              


                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(new ResultMessage
                {
                    Code = "404",
                    Text = $"Erro Interno {e}"
                });
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateActionResultAsync([FromBody] User user)
        {
            try
            {
                var users = await _userService.UpdateUserByIdAsync(user);

                if (users == null)
                {
                    return NotFound(new ResultMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário selecionado!"
                    });
                }
                //var userDtos = UserMapper.MapToSearch(users);                              

                if (users == false)
                {
                    return NotFound(new ResultMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário Alterado!"
                    });

                }
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(new ResultMessage
                {
                    Code = "404",
                    Text = $"Erro Interno {e}"
                });
            }
        }

        [HttpPost("{id}/create")]
        public async Task<IActionResult> CreateActionResultAsync([FromBody] User user)
        {
            try
            {
                var users = await _userService.CreateAsync(user);

                if (users == null)
                {
                    return NotFound(new ResultMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário selecionado!"
                    });
                }
                //var userDtos = UserMapper.MapToSearch(users);                              

                if (users.Erro == true)
                {
                    return NotFound(new ResultMessage
                    {
                        Code = "404",
                        Text = "Nenhum Usuário Alterado!"
                    });

                }
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(new ResultMessage
                {
                    Code = "404",
                    Text = $"Erro Interno {e}"
                });
            }
        }
    }
}

