using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationManagementMobile.Infrastructure.Models;

[Table("Payments")]
public class PaymentDbModel
{
    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? ReservationId { get; set; }

    [ForeignKey(nameof(ReservationId))]
    public ReservationDbModel? Reservation { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
