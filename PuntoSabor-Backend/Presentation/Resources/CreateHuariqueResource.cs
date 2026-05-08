namespace PuntoSabor_Backend.Presentation.Resources;

/**
 * <summary>
 *     Datos necesarios para crear un huarique.
 * </summary>
 */

public record CreateHuariqueResource(
    string Name,
    string Category,
    int CategoryId,
    decimal Price,
    double Rating,
    string District,
    bool Near
);