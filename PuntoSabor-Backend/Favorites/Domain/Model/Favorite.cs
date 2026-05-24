using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Favorites.Domain.Model;

public class Favorite : AuditableEntity
{
    public int UserId { get; set; }
    public int HuariqueId { get; set; }
}
