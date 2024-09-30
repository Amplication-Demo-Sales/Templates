using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrmManagement.Core.Enums;

namespace CrmManagement.Infrastructure.Models;

[Table("Reservations")]
public class ReservationDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? NumberOfGuests { get; set; }

    public List<PaymentDbModel>? Payments { get; set; } = new List<PaymentDbModel>();

    public DateTime? ReservationDate { get; set; }

    public string? RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public RoomDbModel? Room { get; set; } = null;

    public List<ServiceDbModel>? Services { get; set; } = new List<ServiceDbModel>();

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
