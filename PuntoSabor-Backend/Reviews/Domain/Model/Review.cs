using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Reviews.Domain.Model;

/**
 * <summary>
 *     Reseña realizada por un usuario a un huarique.
 * </summary>
 */

public class Review : AuditableEntity
{
    
    public int HuariqueId { get; set; }
    
    public int UserId { get; set; }
    
    public int Rating { get; set; }
    
    public string Comment { get; set; } = null!;
    
    public DateTime CreatedAtReview { get; set; }
}