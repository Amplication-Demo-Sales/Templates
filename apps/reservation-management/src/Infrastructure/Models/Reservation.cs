using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReservationManagement.Core.Enums;

namespace ReservationManagement.Infrastructure.Models;

[Table("Reservations")]
public class ReservationDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? EndDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<PaymentDbModel>? Payments { get; set; } = new List<PaymentDbModel>();

    public DateTime? ReservationDate { get; set; }

    public List<ReviewDbModel>? Reviews { get; set; } = new List<ReviewDbModel>();

    public string? RoomId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public RoomDbModel? Room { get; set; } = null;

    public DateTime? StartDate { get; set; }

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
