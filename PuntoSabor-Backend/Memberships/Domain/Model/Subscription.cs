using PuntoSabor_Backend.Shared.Domain.Model;

namespace PuntoSabor_Backend.Memberships.Domain.Model;

public class Subscription : AuditableEntity
{
    public int UserId { get; set; }

    public string PlanId { get; set; } = null!;

    public Plan? Plan { get; set; }

    public DateTime StartDate { get; set; } = DateTime.UtcNow;

    public DateTime? EndDate { get; set; }

    /// <summary>active | cancelled | expired</summary>
    public string Status { get; set; } = "active";

    public bool IsActive => Status == "active"
        && (EndDate == null || EndDate >= DateTime.UtcNow);
}
