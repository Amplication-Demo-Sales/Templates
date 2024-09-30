namespace ReservationManagementMobile.APIs.Dtos;

public class RoomUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public bool? IsAvailable { get; set; }

    public double? PricePerNight { get; set; }

    public List<string>? Reservations { get; set; }

    public int? RoomNumber { get; set; }

    public string? TypeField { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
