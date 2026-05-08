using Microsoft.EntityFrameworkCore;
using PuntoSabor_Backend.Discovery.Domain.Model;
using PuntoSabor_Backend.Discovery.Domain.Repositories;
using PuntoSabor_Backend.Shared.Infrastructure.Persistence.EFC;

namespace PuntoSabor_Backend.Discovery.Infrastructure.Persistence.EFC.Repositories;

/**
 * <summary>
 *     Implementación del repositorio de categorías utilizando Entity Framework Core.
 * </summary>
 */
public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    
    public async Task<IEnumerable<Category>> ListAsync(CancellationToken ct = default)
    
    {
        return await context.Categories.AsNoTracking().ToListAsync(ct);
    }
}