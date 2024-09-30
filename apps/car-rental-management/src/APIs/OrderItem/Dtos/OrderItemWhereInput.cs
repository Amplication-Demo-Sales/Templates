namespace CarRentalManagementMobile.APIs.Dtos;

public class OrderItemWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public List<string>? Orders { get; set; }

    public double? Price { get; set; }

    public string? Product { get; set; }

    public int? Quantity { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
