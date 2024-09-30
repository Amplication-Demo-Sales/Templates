using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.Infrastructure.Models;

namespace CarRentalManagementMobile.APIs.Extensions;

public static class OrderItemsExtensions
{
    public static OrderItem ToDto(this OrderItemDbModel model)
    {
        return new OrderItem
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Orders = model.Orders?.Select(x => x.Id).ToList(),
            Price = model.Price,
            Product = model.ProductId,
            Quantity = model.Quantity,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OrderItemDbModel ToModel(
        this OrderItemUpdateInput updateDto,
        OrderItemWhereUniqueInput uniqueId
    )
    {
        var orderItem = new OrderItemDbModel
        {
            Id = uniqueId.Id,
            Price = updateDto.Price,
            Quantity = updateDto.Quantity
        };

        if (updateDto.CreatedAt != null)
        {
            orderItem.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Product != null)
        {
            orderItem.ProductId = updateDto.Product;
        }
        if (updateDto.UpdatedAt != null)
        {
            orderItem.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return orderItem;
    }
}
