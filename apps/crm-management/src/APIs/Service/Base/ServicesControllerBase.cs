using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ServicesControllerBase : ControllerBase
{
    protected readonly IServicesService _service;

    public ServicesControllerBase(IServicesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Service
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Service>> CreateService(ServiceCreateInput input)
    {
        var service = await _service.CreateService(input);

        return CreatedAtAction(nameof(Service), new { id = service.Id }, service);
    }

    /// <summary>
    /// Delete one Service
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteService([FromRoute()] ServiceWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteService(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Services
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Service>>> Services(
        [FromQuery()] ServiceFindManyArgs filter
    )
    {
        return Ok(await _service.Services(filter));
    }

    /// <summary>
    /// Meta data about Service records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ServicesMeta(
        [FromQuery()] ServiceFindManyArgs filter
    )
    {
        return Ok(await _service.ServicesMeta(filter));
    }

    /// <summary>
    /// Get one Service
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Service>> Service([FromRoute()] ServiceWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Service(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Service
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateService(
        [FromRoute()] ServiceWhereUniqueInput uniqueId,
        [FromQuery()] ServiceUpdateInput serviceUpdateDto
    )
    {
        try
        {
            await _service.UpdateService(uniqueId, serviceUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Reservation record for Service
    /// </summary>
    [HttpGet("{Id}/reservation")]
    public async Task<ActionResult<List<Reservation>>> GetReservation(
        [FromRoute()] ServiceWhereUniqueInput uniqueId
    )
    {
        var reservation = await _service.GetReservation(uniqueId);
        return Ok(reservation);
    }
}
