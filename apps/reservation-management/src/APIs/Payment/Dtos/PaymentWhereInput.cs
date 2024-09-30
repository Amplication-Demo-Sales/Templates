using ReservationManagement.Core.Enums;

namespace ReservationManagement.APIs.Dtos;

public class PaymentWhereInput
{
    public double? Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Reservation { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
