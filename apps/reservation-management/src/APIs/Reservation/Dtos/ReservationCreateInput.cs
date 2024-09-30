using ReservationManagement.Core.Enums;

namespace ReservationManagement.APIs.Dtos;

public class ReservationCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Id { get; set; }

    public List<Payment>? Payments { get; set; }

    public DateTime? ReservationDate { get; set; }

    public List<Review>? Reviews { get; set; }

    public Room? Room { get; set; }

    public DateTime? StartDate { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
