using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationManagement.Infrastructure.Models;

[Table("Reviews")]
public class ReviewDbModel
{
    [StringLength(1000)]
    public string? Comment { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? Rating { get; set; }

    public string? ReservationId { get; set; }

    [ForeignKey(nameof(ReservationId))]
    public ReservationDbModel? Reservation { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
