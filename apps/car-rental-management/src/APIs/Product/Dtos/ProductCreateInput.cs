namespace CarRentalManagementMobile.APIs.Dtos;

public class ProductCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    public double? Price { get; set; }

    public int? Stock { get; set; }

    public DateTime UpdatedAt { get; set; }
}
