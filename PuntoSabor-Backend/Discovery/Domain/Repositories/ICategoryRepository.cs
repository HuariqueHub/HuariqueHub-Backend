using PuntoSabor_Backend.Discovery.Domain.Model;

namespace PuntoSabor_Backend.Discovery.Domain.Repositories;

/**
 * <summary>
 *     Repositorio para obtener la lista de categorías disponibles.
 * </summary>
 */

public interface ICategoryRepository

{
    Task<IEnumerable<Category>> ListAsync(CancellationToken ct = default);
}