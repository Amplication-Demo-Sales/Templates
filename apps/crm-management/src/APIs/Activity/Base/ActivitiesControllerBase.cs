using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ActivitiesControllerBase : ControllerBase
{
    protected readonly IActivitiesService _service;

    public ActivitiesControllerBase(IActivitiesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Activity
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Activity>> CreateActivity(ActivityCreateInput input)
    {
        var activity = await _service.CreateActivity(input);

        return CreatedAtAction(nameof(Activity), new { id = activity.Id }, activity);
    }

    /// <summary>
    /// Delete one Activity
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteActivity([FromRoute()] ActivityWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteActivity(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Activities
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Activity>>> Activities(
        [FromQuery()] ActivityFindManyArgs filter
    )
    {
        return Ok(await _service.Activities(filter));
    }

    /// <summary>
    /// Meta data about Activity records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ActivitiesMeta(
        [FromQuery()] ActivityFindManyArgs filter
    )
    {
        return Ok(await _service.ActivitiesMeta(filter));
    }

    /// <summary>
    /// Get one Activity
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Activity>> Activity(
        [FromRoute()] ActivityWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Activity(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Activity
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateActivity(
        [FromRoute()] ActivityWhereUniqueInput uniqueId,
        [FromQuery()] ActivityUpdateInput activityUpdateDto
    )
    {
        try
        {
            await _service.UpdateActivity(uniqueId, activityUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Contact record for Activity
    /// </summary>
    [HttpGet("{Id}/contact")]
    public async Task<ActionResult<List<Contact>>> GetContact(
        [FromRoute()] ActivityWhereUniqueInput uniqueId
    )
    {
        var contact = await _service.GetContact(uniqueId);
        return Ok(contact);
    }

    /// <summary>
    /// Get a Customer record for Activity
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] ActivityWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Get a Lead record for Activity
    /// </summary>
    [HttpGet("{Id}/lead")]
    public async Task<ActionResult<List<Lead>>> GetLead(
        [FromRoute()] ActivityWhereUniqueInput uniqueId
    )
    {
        var lead = await _service.GetLead(uniqueId);
        return Ok(lead);
    }

    /// <summary>
    /// Get a Opportunity record for Activity
    /// </summary>
    [HttpGet("{Id}/opportunity")]
    public async Task<ActionResult<List<Opportunity>>> GetOpportunity(
        [FromRoute()] ActivityWhereUniqueInput uniqueId
    )
    {
        var opportunity = await _service.GetOpportunity(uniqueId);
        return Ok(opportunity);
    }
}
