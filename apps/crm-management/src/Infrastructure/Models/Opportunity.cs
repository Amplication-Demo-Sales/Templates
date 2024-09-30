using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CrmManagement.Core.Enums;

namespace CrmManagement.Infrastructure.Models;

[Table("Opportunities")]
public class OpportunityDbModel
{
    public List<ActivityDbModel>? Activities { get; set; } = new List<ActivityDbModel>();

    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    public DateTime? CloseDate { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public StageEnum? Stage { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
