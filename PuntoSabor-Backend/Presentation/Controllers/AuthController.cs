using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PuntoSabor_Backend.Auth.Application.Services;
using PuntoSabor_Backend.Auth.Domain.Repositories;
using PuntoSabor_Backend.Presentation.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace PuntoSabor_Backend.Presentation.Controllers;

[ApiController]
[Route("auth")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication Endpoints.")]
public class AuthController(
    IUserRepository users,
    ITokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Login", "Authenticate with email and password, returns a JWT token.", OperationId = "Login")]
    [SwaggerResponse(200, "Autenticación exitosa.", typeof(AuthResponseResource))]
    [SwaggerResponse(400, "Datos inválidos.", typeof(ErrorResource))]
    [SwaggerResponse(401, "Credenciales incorrectas.", typeof(ErrorResource))]
    public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken ct)
    {
        if (resource is null)
            return BadRequest(new ErrorResource("Body inválido."));

        if (string.IsNullOrWhiteSpace(resource.Email) || string.IsNullOrWhiteSpace(resource.Password))
            return BadRequest(new ErrorResource("Email y contraseña son obligatorios."));

        var matches = await users.FindByEmailAsync(resource.Email.Trim(), ct);
        var user = matches.FirstOrDefault();

        if (user is null || !BCrypt.Net.BCrypt.Verify(resource.Password, user.PasswordHash))
            return Unauthorized(new ErrorResource("Credenciales inválidas."));

        var token = tokenService.GenerateToken(user);
        return Ok(new AuthResponseResource(user.Id, user.Name, user.Email,
            user.Role.ToString().ToLowerInvariant(), token));
    }
}
