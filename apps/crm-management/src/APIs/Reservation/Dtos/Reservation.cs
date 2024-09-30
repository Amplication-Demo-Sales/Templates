using CrmManagement.Core.Enums;

namespace CrmManagement.APIs.Dtos;

public class Reservation
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string Id { get; set; }

    public int? NumberOfGuests { get; set; }

    public List<string>? Payments { get; set; }

    public DateTime? ReservationDate { get; set; }

    public string? Room { get; set; }

    public List<string>? Services { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
