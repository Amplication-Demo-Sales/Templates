using CarRentalManagement.APIs;
using CarRentalManagement.APIs.Common;
using CarRentalManagement.APIs.Dtos;
using CarRentalManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrdersControllerBase : ControllerBase
{
    protected readonly IOrdersService _service;

    public OrdersControllerBase(IOrdersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Order>> CreateOrder(OrderCreateInput input)
    {
        var order = await _service.CreateOrder(input);

        return CreatedAtAction(nameof(Order), new { id = order.Id }, order);
    }

    /// <summary>
    /// Delete one Order
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute()] OrderWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteOrder(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Order>>> Orders([FromQuery()] OrderFindManyArgs filter)
    {
        return Ok(await _service.Orders(filter));
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrdersMeta([FromQuery()] OrderFindManyArgs filter)
    {
        return Ok(await _service.OrdersMeta(filter));
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Order>> Order([FromRoute()] OrderWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Order(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateOrder(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] OrderUpdateInput orderUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrder(uniqueId, orderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] OrderWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
