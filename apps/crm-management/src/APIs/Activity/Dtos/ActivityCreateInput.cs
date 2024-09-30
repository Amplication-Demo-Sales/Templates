namespace CrmManagement.APIs.Dtos;

public class ActivityCreateInput
{
    public DateTime? ActivityDate { get; set; }

    public Contact? Contact { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public Lead? Lead { get; set; }

    public Opportunity? Opportunity { get; set; }

    public string? RelatedTo { get; set; }

    public string? Subject { get; set; }

    public DateTime UpdatedAt { get; set; }
}
