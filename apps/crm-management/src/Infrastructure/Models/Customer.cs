using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmManagement.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    public List<ActivityDbModel>? Activities { get; set; } = new List<ActivityDbModel>();

    [StringLength(1000)]
    public string? Address { get; set; }

    public List<ContactDbModel>? Contacts { get; set; } = new List<ContactDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<LeadDbModel>? Leads { get; set; } = new List<LeadDbModel>();

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<OpportunityDbModel>? Opportunities { get; set; } = new List<OpportunityDbModel>();

    [StringLength(1000)]
    public string? Phone { get; set; }

    public List<ReservationDbModel>? Reservations { get; set; } = new List<ReservationDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
