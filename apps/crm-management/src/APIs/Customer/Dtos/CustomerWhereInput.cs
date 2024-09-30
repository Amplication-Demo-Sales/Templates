namespace CrmManagement.APIs.Dtos;

public class CustomerWhereInput
{
    public List<string>? Activities { get; set; }

    public string? Address { get; set; }

    public List<string>? Contacts { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public List<string>? Leads { get; set; }

    public string? Name { get; set; }

    public List<string>? Opportunities { get; set; }

    public string? Phone { get; set; }

    public List<string>? Reservations { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
