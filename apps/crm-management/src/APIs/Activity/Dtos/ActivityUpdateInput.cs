namespace CrmManagement.APIs.Dtos;

public class ActivityUpdateInput
{
    public DateTime? ActivityDate { get; set; }

    public string? Contact { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Lead { get; set; }

    public string? Opportunity { get; set; }

    public string? RelatedTo { get; set; }

    public string? Subject { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
