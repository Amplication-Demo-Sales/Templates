using ReservationManagement.Core.Enums;

namespace ReservationManagement.APIs.Dtos;

public class ReservationWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Id { get; set; }

    public List<string>? Payments { get; set; }

    public DateTime? ReservationDate { get; set; }

    public List<string>? Reviews { get; set; }

    public string? Room { get; set; }

    public DateTime? StartDate { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? User { get; set; }
}
