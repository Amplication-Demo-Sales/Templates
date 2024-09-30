using ReservationManagement.Core.Enums;

namespace ReservationManagement.APIs.Dtos;

public class RoomUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public double? Price { get; set; }

    public List<string>? Reservations { get; set; }

    public string? RoomNumber { get; set; }

    public TypeFieldEnum? TypeField { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
