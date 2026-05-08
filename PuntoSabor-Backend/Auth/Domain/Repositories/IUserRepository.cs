using PuntoSabor_Backend.Auth.Domain.Model;

namespace PuntoSabor_Backend.Auth.Domain.Repositories;

/**
 * <summary>
 *     Repositorio para gestionar usuarios y realizar búsquedas por correo.
 * </summary>
 */

public interface IUserRepository
{
    
    Task<IEnumerable<User>> FindByEmailAsync(
        string? email,
        CancellationToken ct = default);

    Task AddAsync(
        User user,
        CancellationToken ct = default);
    
}