using CrmManagement.Core.Enums;

namespace CrmManagement.APIs.Dtos;

public class OpportunityWhereInput
{
    public List<string>? Activities { get; set; }

    public double? Amount { get; set; }

    public DateTime? CloseDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public StageEnum? Stage { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
