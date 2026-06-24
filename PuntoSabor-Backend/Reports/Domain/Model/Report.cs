using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Reports.Domain.Model;

/**
 * <summary>
 *     Reporte de información incorrecta sobre un huarique (US21).
 * </summary>
 */
public class Report : AuditableEntity
{
    public int HuariqueId { get; set; }

    public int UserId { get; set; }

    /// Motivo del reporte (qué dato está incorrecto).
    public string Reason { get; set; } = null!;

    /// pending | reviewed
    public string Status { get; set; } = "pending";
}
