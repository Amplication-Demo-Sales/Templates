using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationManagementMobile.Infrastructure.Models;

[Table("Rooms")]
public class RoomDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public bool? IsAvailable { get; set; }

    [Range(-999999999, 999999999)]
    public double? PricePerNight { get; set; }

    public List<ReservationDbModel>? Reservations { get; set; } = new List<ReservationDbModel>();

    [Range(-999999999, 999999999)]
    public int? RoomNumber { get; set; }

    [StringLength(1000)]
    public string? TypeField { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
