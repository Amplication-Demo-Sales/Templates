using CrmManagement.Core.Enums;

namespace CrmManagement.APIs.Dtos;

public class ReservationCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public int? NumberOfGuests { get; set; }

    public List<Payment>? Payments { get; set; }

    public DateTime? ReservationDate { get; set; }

    public Room? Room { get; set; }

    public List<Service>? Services { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
