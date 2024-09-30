using CrmManagement.Core.Enums;

namespace CrmManagement.APIs.Dtos;

public class LeadCreateInput
{
    public List<Activity>? Activities { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public SourceEnum? Source { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
