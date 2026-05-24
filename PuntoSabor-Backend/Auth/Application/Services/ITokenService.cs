using PuntoSabor_Backend.Auth.Domain.Model;

namespace PuntoSabor_Backend.Auth.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
