using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Auth.Domain.Model;

/**
 * <summary>
 *     Representa a un usuario dentro del sistema, con nombre y correo electrónico.
 * </summary>
 */

public class User : AuditableEntity
{
    
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    
}