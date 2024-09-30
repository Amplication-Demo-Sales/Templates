using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalManagementMobile.Infrastructure.Models;

[Table("Orders")]
public class OrderDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderItemId { get; set; }

    [ForeignKey(nameof(OrderItemId))]
    public OrderItemDbModel? OrderItem { get; set; } = null;

    [StringLength(1000)]
    public string? Status { get; set; }

    [Range(-999999999, 999999999)]
    public double? Total { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
