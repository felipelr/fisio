using System.Security;
using fisio.domain.Api.Models;
using fisio.domain.Api.Services;
using fisio.domain.Commands.Common;
using fisio.domain.Commands.Users;
using fisio.domain.Dtos;
using fisio.domain.Entities;
using fisio.domain.Handlers.Users;
using fisio.domain.Mappers.Interfaces;
using fisio.domain.Repositories;
using fisio.domain.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
                    [FromServices] IRefreshTokenRepository refreshTokenRepository,
                    [FromServices] IMapperConfig mapper,
                    [FromServices] IUnitOfWork unitOfWork,
                    CancellationToken cancellationToken,
                    [FromBody] UserLogin model)
    {
        try
        {
            var user = await userRepository.Login(model.Email, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário e/ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            var refreshToken = TokenService.GenerateRefreshToken();
            refreshTokenRepository.Create(new RefreshToken(user.Id, refreshToken));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var userDTO = mapper.GetMapper().Map<UserDto>(user);

            return Ok(new
            {
                user = userDTO,
                token,
                refreshToken
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar o login");
            return Problem(detail: ex.StackTrace, title: ex.Message);
        }
    }

    [HttpPost]
    [Route("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Refresh(
        [FromServices] IRefreshTokenRepository refreshTokenRepository,
        [FromServices] IUnitOfWork unitOfWork,
        CancellationToken cancellationToken,
        string token,
        string refreshToken)
    {
        try
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(token);
            if (principal.Identity == null)
                return NotFound(new { message = "Token invalido." });

            var key = principal.Identity.Name!;
            var savedRefreshToken = await refreshTokenRepository.GetByKey(key);

            if (savedRefreshToken == null)
                return NotFound(new { message = "Token invalido." });

            if (savedRefreshToken.Token == refreshToken)
                return Problem(title: "Refresh token invalido");

            var newJwtToken = TokenService.GenerateToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();
            if (savedRefreshToken != null)
                refreshTokenRepository.Delete(savedRefreshToken);

            refreshTokenRepository.Create(new RefreshToken(key, newRefreshToken));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar refresh token");
            return Problem(detail: ex.StackTrace, title: ex.Message);
        }
    }

    [HttpPost]
    // [Authorize(Roles = "adm")]
    public async Task<ActionResult<dynamic>> Create(
                    [FromServices] CreateUserHandler createUserHandler,
                    [FromBody] CreateUserCommand createUserCommand,
                    CancellationToken cancellationToken = default)
    {
        try
        {
            if (createUserCommand == null)
                return BadRequest(new { message = "Informações inválidas" });

            var result = (GenericCommandResult)await createUserHandler.Handle(createUserCommand, cancellationToken);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar usuário");
            return Problem(detail: ex.StackTrace, title: ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<dynamic>> GetAll([FromServices] IUserRepository userRepository)
    {
        try
        {
            _logger.LogInformation("Log buscando todos os usuários.");
            var result = await userRepository.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar usuários");
            return Problem(detail: ex.StackTrace, title: ex.Message);
        }
    }
}
