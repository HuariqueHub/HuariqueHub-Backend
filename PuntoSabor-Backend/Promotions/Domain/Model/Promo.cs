using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Promotions.Domain.Model;

public class Promo : AuditableEntity
{
    public string Title { get; set; } = null!;
    public string Note { get; set; } = null!;

    // Clasificación
    public string Type { get; set; } = "otro";    // 2x1 | descuento | menu | happy-hour | otro
    public int Discount { get; set; } = 0;        // porcentaje (0 si no aplica)
    public string? Code { get; set; }             // código de canje opcional

    // Vigencia
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Uso
    public int? MaxUses { get; set; }             // null = ilimitado
    public int CurrentUses { get; set; } = 0;

    // Asociación y media
    public int? HuariqueId { get; set; }
    public string? ImageUrl { get; set; }

    public bool IsActive =>
        (StartDate == null || StartDate <= DateTime.UtcNow) &&
        (EndDate   == null || EndDate   >= DateTime.UtcNow) &&
        (MaxUses   == null || CurrentUses < MaxUses);
}
