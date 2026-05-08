using PuntoSabor_Backend.Promotions.Domain.Model;

namespace PuntoSabor_Backend.Promotions.Domain.Repositories;

/**
 * <summary>
 *     Repositorio para obtener la lista de promociones.
 * </summary>
 */

public interface IPromoRepository
{
    Task<IEnumerable<Promo>> ListAsync(CancellationToken ct = default);
}