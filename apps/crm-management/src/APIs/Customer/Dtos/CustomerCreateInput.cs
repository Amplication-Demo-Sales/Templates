namespace CrmManagement.APIs.Dtos;

public class CustomerCreateInput
{
    public List<Activity>? Activities { get; set; }

    public string? Address { get; set; }

    public List<Contact>? Contacts { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public List<Lead>? Leads { get; set; }

    public string? Name { get; set; }

    public List<Opportunity>? Opportunities { get; set; }

    public string? Phone { get; set; }

    public List<Reservation>? Reservations { get; set; }

    public DateTime UpdatedAt { get; set; }
}
