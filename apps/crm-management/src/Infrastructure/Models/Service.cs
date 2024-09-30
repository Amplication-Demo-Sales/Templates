using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmManagement.Infrastructure.Models;

[Table("Services")]
public class ServiceDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    public string? ReservationId { get; set; }

    [ForeignKey(nameof(ReservationId))]
    public ReservationDbModel? Reservation { get; set; } = null;

    [StringLength(1000)]
    public string? ServiceName { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
