namespace CrmManagement.APIs.Dtos;

public class Service
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; }

    public double? Price { get; set; }

    public string? Reservation { get; set; }

    public string? ServiceName { get; set; }

    public DateTime UpdatedAt { get; set; }
}
