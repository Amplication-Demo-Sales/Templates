using CarRentalManagementMobile.APIs;
using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrderItemsControllerBase : ControllerBase
{
    protected readonly IOrderItemsService _service;

    public OrderItemsControllerBase(IOrderItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one OrderItem
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItemCreateInput input)
    {
        var orderItem = await _service.CreateOrderItem(input);

        return CreatedAtAction(nameof(OrderItem), new { id = orderItem.Id }, orderItem);
    }

    /// <summary>
    /// Delete one OrderItem
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteOrderItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteOrderItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<OrderItem>>> OrderItems(
        [FromQuery()] OrderItemFindManyArgs filter
    )
    {
        return Ok(await _service.OrderItems(filter));
    }

    /// <summary>
    /// Meta data about OrderItem records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrderItemsMeta(
        [FromQuery()] OrderItemFindManyArgs filter
    )
    {
        return Ok(await _service.OrderItemsMeta(filter));
    }

    /// <summary>
    /// Get one OrderItem
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<OrderItem>> OrderItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.OrderItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one OrderItem
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateOrderItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId,
        [FromQuery()] OrderItemUpdateInput orderItemUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrderItem(uniqueId, orderItemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Orders records to OrderItem
    /// </summary>
    [HttpPost("{Id}/orders")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId,
        [FromQuery()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.ConnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Orders records from OrderItem
    /// </summary>
    [HttpDelete("{Id}/orders")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.DisconnectOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Orders records for OrderItem
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<Order>>> FindOrders(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId,
        [FromQuery()] OrderFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOrders(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Orders records for OrderItem
    /// </summary>
    [HttpPatch("{Id}/orders")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId,
        [FromBody()] OrderWhereUniqueInput[] ordersId
    )
    {
        try
        {
            await _service.UpdateOrders(uniqueId, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Product record for OrderItem
    /// </summary>
    [HttpGet("{Id}/product")]
    public async Task<ActionResult<List<Product>>> GetProduct(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        var product = await _service.GetProduct(uniqueId);
        return Ok(product);
    }
}
