using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.Infrastructure.Models;

namespace CarRentalManagementMobile.APIs.Extensions;

public static class OrdersExtensions
{
    public static Order ToDto(this OrderDbModel model)
    {
        return new Order
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            OrderDate = model.OrderDate,
            OrderItem = model.OrderItemId,
            Status = model.Status,
            Total = model.Total,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OrderDbModel ToModel(
        this OrderUpdateInput updateDto,
        OrderWhereUniqueInput uniqueId
    )
    {
        var order = new OrderDbModel
        {
            Id = uniqueId.Id,
            OrderDate = updateDto.OrderDate,
            Status = updateDto.Status,
            Total = updateDto.Total
        };

        if (updateDto.CreatedAt != null)
        {
            order.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.OrderItem != null)
        {
            order.OrderItemId = updateDto.OrderItem;
        }
        if (updateDto.UpdatedAt != null)
        {
            order.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return order;
    }
}
