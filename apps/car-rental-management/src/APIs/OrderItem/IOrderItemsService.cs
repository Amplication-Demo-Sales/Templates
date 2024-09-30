using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.APIs.Dtos;

namespace CarRentalManagementMobile.APIs;

public interface IOrderItemsService
{
    /// <summary>
    /// Create one OrderItem
    /// </summary>
    public Task<OrderItem> CreateOrderItem(OrderItemCreateInput orderitem);

    /// <summary>
    /// Delete one OrderItem
    /// </summary>
    public Task DeleteOrderItem(OrderItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    public Task<List<OrderItem>> OrderItems(OrderItemFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about OrderItem records
    /// </summary>
    public Task<MetadataDto> OrderItemsMeta(OrderItemFindManyArgs findManyArgs);

    /// <summary>
    /// Get one OrderItem
    /// </summary>
    public Task<OrderItem> OrderItem(OrderItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one OrderItem
    /// </summary>
    public Task UpdateOrderItem(OrderItemWhereUniqueInput uniqueId, OrderItemUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Orders records to OrderItem
    /// </summary>
    public Task ConnectOrders(OrderItemWhereUniqueInput uniqueId, OrderWhereUniqueInput[] ordersId);

    /// <summary>
    /// Disconnect multiple Orders records from OrderItem
    /// </summary>
    public Task DisconnectOrders(
        OrderItemWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] ordersId
    );

    /// <summary>
    /// Find multiple Orders records for OrderItem
    /// </summary>
    public Task<List<Order>> FindOrders(
        OrderItemWhereUniqueInput uniqueId,
        OrderFindManyArgs OrderFindManyArgs
    );

    /// <summary>
    /// Update multiple Orders records for OrderItem
    /// </summary>
    public Task UpdateOrders(OrderItemWhereUniqueInput uniqueId, OrderWhereUniqueInput[] ordersId);

    /// <summary>
    /// Get a Product record for OrderItem
    /// </summary>
    public Task<Product> GetProduct(OrderItemWhereUniqueInput uniqueId);
}
