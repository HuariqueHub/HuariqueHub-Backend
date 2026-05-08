namespace PuntoSabor_Backend.Presentation.Resources;

/**
 * <summary>
 *     Representación de un huarique para respuestas de la API.
 * </summary>
 */

public record HuariqueResource(
    int Id,
    string Name,
    string Category,
    int CategoryId,
    decimal Price,
    double Rating,
    string District,
    bool Near,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);