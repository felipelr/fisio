using fisio.domain.Api.Models;
using fisio.domain.Api.Services;
using fisio.domain.Commands.Common;
using fisio.domain.Commands.Users;
using fisio.domain.Dtos;
using fisio.domain.Handlers.Users;
using fisio.domain.Mappers.Interfaces;
using fisio.domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fisio.api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate(
                    [FromServices] IUserRepository userRepository,
                    [FromServices] IMapperConfig mapper,
                    [FromBody] UserLogin model)
    {
        try
        {
            var user = await userRepository.Login(model.Email, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário e/ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            var userDTO = mapper.GetMapper().Map<UserDto>(user);

            return Ok(new
            {
                user = userDTO,
                token = token
            });
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.StackTrace, title: ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "adm")]
    public async Task<ActionResult<dynamic>> Create(
                    [FromServices] CreateUserHandler createUserHandler,
                    [FromBody] CreateUserCommand createUserCommand)
    {
        try
        {
            if (createUserCommand == null)
                return BadRequest(new { message = "Informações inválidas" });

            var result = (GenericCommandResult)await createUserHandler.Handle(createUserCommand);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.StackTrace, title: ex.Message);
        }
    }
}
