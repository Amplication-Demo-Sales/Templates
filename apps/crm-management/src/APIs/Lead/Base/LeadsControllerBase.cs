using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class LeadsControllerBase : ControllerBase
{
    protected readonly ILeadsService _service;

    public LeadsControllerBase(ILeadsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Lead
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Lead>> CreateLead(LeadCreateInput input)
    {
        var lead = await _service.CreateLead(input);

        return CreatedAtAction(nameof(Lead), new { id = lead.Id }, lead);
    }

    /// <summary>
    /// Delete one Lead
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteLead([FromRoute()] LeadWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteLead(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Leads
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Lead>>> Leads([FromQuery()] LeadFindManyArgs filter)
    {
        return Ok(await _service.Leads(filter));
    }

    /// <summary>
    /// Meta data about Lead records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> LeadsMeta([FromQuery()] LeadFindManyArgs filter)
    {
        return Ok(await _service.LeadsMeta(filter));
    }

    /// <summary>
    /// Get one Lead
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Lead>> Lead([FromRoute()] LeadWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Lead(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Lead
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateLead(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromQuery()] LeadUpdateInput leadUpdateDto
    )
    {
        try
        {
            await _service.UpdateLead(uniqueId, leadUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Activities records to Lead
    /// </summary>
    [HttpPost("{Id}/activities")]
    public async Task<ActionResult> ConnectActivities(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromQuery()] ActivityWhereUniqueInput[] activitiesId
    )
    {
        try
        {
            await _service.ConnectActivities(uniqueId, activitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Activities records from Lead
    /// </summary>
    [HttpDelete("{Id}/activities")]
    public async Task<ActionResult> DisconnectActivities(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromBody()] ActivityWhereUniqueInput[] activitiesId
    )
    {
        try
        {
            await _service.DisconnectActivities(uniqueId, activitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Activities records for Lead
    /// </summary>
    [HttpGet("{Id}/activities")]
    public async Task<ActionResult<List<Activity>>> FindActivities(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromQuery()] ActivityFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindActivities(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Activities records for Lead
    /// </summary>
    [HttpPatch("{Id}/activities")]
    public async Task<ActionResult> UpdateActivities(
        [FromRoute()] LeadWhereUniqueInput uniqueId,
        [FromBody()] ActivityWhereUniqueInput[] activitiesId
    )
    {
        try
        {
            await _service.UpdateActivities(uniqueId, activitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Lead
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] LeadWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
