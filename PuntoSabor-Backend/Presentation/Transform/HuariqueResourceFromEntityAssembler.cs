using PuntoSabor_Backend.Discovery.Domain.Model;
using PuntoSabor_Backend.Presentation.Resources;

namespace PuntoSabor_Backend.Presentation.Transform;

/**
 * <summary>
 *     Convierte una entidad Huarique en su recurso de respuesta.
 * </summary>
 */


public static class HuariqueResourceFromEntityAssembler
{
    public static HuariqueResource ToResourceFromEntity(Huarique entity) =>
        new(
            entity.Id,
            entity.Name,
            entity.Category,
            entity.CategoryId,
            entity.Price,
            entity.Rating,
            entity.District,
            entity.Near,
            entity.CreatedAt,
            entity.UpdatedAt
        );
}