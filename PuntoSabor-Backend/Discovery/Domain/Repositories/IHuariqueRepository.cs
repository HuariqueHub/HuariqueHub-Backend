using PuntoSabor_Backend.Discovery.Domain.Model;

namespace PuntoSabor_Backend.Discovery.Domain.Repositories;

/**
 * <summary>
 *     Repositorio para buscar, obtener y gestionar huariques dentro del sistema.
 * </summary>
 */

public interface IHuariqueRepository
{
    Task<IEnumerable<Huarique>> SearchAsync(
        string? q,
        bool? near,
        CancellationToken ct = default);

    Task<Huarique?> FindByIdAsync(
        int id,
        CancellationToken ct = default);

    Task AddAsync(
        Huarique huarique,
        CancellationToken ct = default);

    Task PatchAsync(
        Huarique huarique,
        IDictionary<string, object> patch,
        CancellationToken ct = default);
}