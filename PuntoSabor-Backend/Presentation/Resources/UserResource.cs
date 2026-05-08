namespace PuntoSabor_Backend.Presentation.Resources;

/**
 * <summary>
 *     Representación de un usuario para respuestas de la API.
 * </summary>
 */

public record UserResource(
    int Id,
    string Name,
    string Email,
    DateTime CreatedAt,
    DateTime? UpdatedAt);