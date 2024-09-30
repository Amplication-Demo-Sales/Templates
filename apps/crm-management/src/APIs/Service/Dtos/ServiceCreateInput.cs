namespace CrmManagement.APIs.Dtos;

public class ServiceCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public double? Price { get; set; }

    public Reservation? Reservation { get; set; }

    public string? ServiceName { get; set; }

    public DateTime UpdatedAt { get; set; }
}
