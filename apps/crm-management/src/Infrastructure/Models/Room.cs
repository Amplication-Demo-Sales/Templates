using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrmManagement.Core.Enums;

namespace CrmManagement.Infrastructure.Models;

[Table("Rooms")]
public class RoomDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    public List<ReservationDbModel>? Reservations { get; set; } = new List<ReservationDbModel>();

    [StringLength(1000)]
    public string? RoomNumber { get; set; }

    public TypeFieldEnum? TypeField { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
