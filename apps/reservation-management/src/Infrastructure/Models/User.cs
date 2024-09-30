using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationManagement.Infrastructure.Models;

[Table("Users")]
public class UserDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? LastName { get; set; }

    [StringLength(1000)]
    public string? Password { get; set; }

    public List<ReservationDbModel>? Reservations { get; set; } = new List<ReservationDbModel>();

    public List<ReviewDbModel>? Reviews { get; set; } = new List<ReviewDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Username { get; set; }
}
