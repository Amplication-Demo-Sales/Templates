namespace CarRentalManagementMobile.APIs.Dtos;

public class RoleUpdateInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<string>? Users { get; set; }
}
