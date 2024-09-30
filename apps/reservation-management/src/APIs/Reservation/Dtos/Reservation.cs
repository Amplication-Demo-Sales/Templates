namespace ReservationManagementMobile.APIs.Dtos;

public class Reservation
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? EndDate { get; set; }

    public string Id { get; set; }

    public List<string>? Payments { get; set; }

    public string? Room { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime UpdatedAt { get; set; }
}
