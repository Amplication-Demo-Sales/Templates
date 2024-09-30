namespace CarRentalManagementMobile.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public OrderItem? OrderItem { get; set; }

    public string? Status { get; set; }

    public double? Total { get; set; }

    public DateTime UpdatedAt { get; set; }
}
