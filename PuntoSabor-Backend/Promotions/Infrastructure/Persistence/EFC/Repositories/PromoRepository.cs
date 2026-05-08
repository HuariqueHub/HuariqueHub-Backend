using Microsoft.EntityFrameworkCore;
using PuntoSabor_Backend.Promotions.Domain.Model;
using PuntoSabor_Backend.Promotions.Domain.Repositories;
using PuntoSabor_Backend.Shared.Infrastructure.Persistence.EFC;

namespace PuntoSabor_Backend.Promotions.Infrastructure.Persistence.EFC.Repositories;

/**
 * <summary>
 *     Implementación del repositorio de promociones usando Entity Framework.
 * </summary>
 */

public class PromoRepository(AppDbContext context) : IPromoRepository
{
    public async Task<IEnumerable<Promo>> ListAsync(CancellationToken ct = default)
    {
        return await context.Promos.AsNoTracking().ToListAsync(ct);
    }
}