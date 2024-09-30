using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalManagementMobile.Infrastructure.Models;

[Table("OrderItems")]
public class OrderItemDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<OrderDbModel>? Orders { get; set; } = new List<OrderDbModel>();

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    public string? ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public ProductDbModel? Product { get; set; } = null;

    [Range(-999999999, 999999999)]
    public int? Quantity { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
