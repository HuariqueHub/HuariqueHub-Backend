using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Notifications.Domain.Model;

/**
 * <summary>
 *     Notificación dirigida a un usuario (US12): p. ej. el dueño recibe un
 *     aviso cuando su huarique recibe una nueva reseña.
 * </summary>
 */
public class Notification : AuditableEntity
{
    /// Destinatario de la notificación.
    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool IsRead { get; set; }
}
