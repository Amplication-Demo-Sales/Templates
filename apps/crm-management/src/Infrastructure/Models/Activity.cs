using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmManagement.Infrastructure.Models;

[Table("Activities")]
public class ActivityDbModel
{
    public DateTime? ActivityDate { get; set; }

    public string? ContactId { get; set; }

    [ForeignKey(nameof(ContactId))]
    public ContactDbModel? Contact { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? LeadId { get; set; }

    [ForeignKey(nameof(LeadId))]
    public LeadDbModel? Lead { get; set; } = null;

    public string? OpportunityId { get; set; }

    [ForeignKey(nameof(OpportunityId))]
    public OpportunityDbModel? Opportunity { get; set; } = null;

    [StringLength(1000)]
    public string? RelatedTo { get; set; }

    [StringLength(1000)]
    public string? Subject { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
