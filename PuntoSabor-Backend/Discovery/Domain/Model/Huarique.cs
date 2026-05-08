using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Discovery.Domain.Model;

/**
 * <summary>
 *     Representa un huarique dentro del sistema, incluyendo su categoría,
 *     precio, ubicación, calificación y datos de auditoría heredados.
 * </summary>
 */
public class Huarique : AuditableEntity
{
    public string Name { get; set; } = null!;
    
    public string Category { get; set; } = null!;
    
    public int CategoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public double Rating { get; set; }
    
    public string District { get; set; } = null!;
    
    public bool Near { get; set; }
}