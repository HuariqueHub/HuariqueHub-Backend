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

    // Coordenadas geográficas — opcionales, usadas para mapas y rutas
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public int? OwnerId { get; set; }

    // Campos de detalle — opcionales, gestionados por el propietario
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpenAt { get; set; }
    public string? CloseAt { get; set; }
    public bool DeliveryAvailable { get; set; } = false;
    public bool TakeawayAvailable { get; set; } = false;
    public bool DineInAvailable { get; set; } = true;
}