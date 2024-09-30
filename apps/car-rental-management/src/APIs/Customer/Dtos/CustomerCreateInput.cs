namespace CarRentalManagement.APIs.Dtos;

public class CustomerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public List<Order>? Orders { get; set; }

    public DateTime UpdatedAt { get; set; }
}
