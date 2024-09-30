using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OpportunitiesControllerBase : ControllerBase
{
    protected readonly IOpportunitiesService _service;

    public OpportunitiesControllerBase(IOpportunitiesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Opportunity
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Opportunity>> CreateOpportunity(OpportunityCreateInput input)
    {
        var opportunity = await _service.CreateOpportunity(input);

        return CreatedAtAction(nameof(Opportunity), new { id = opportunity.Id }, opportunity);
    }

    /// <summary>
    /// Delete one Opportunity
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteOpportunity(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteOpportunity(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Opportunities
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Opportunity>>> Opportunities(
        [FromQuery()] OpportunityFindManyArgs filter
    )
    {
        return Ok(await _service.Opportunities(filter));
    }

    /// <summary>
    /// Meta data about Opportunity records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OpportunitiesMeta(
        [FromQuery()] OpportunityFindManyArgs filter
    )
    {
        return Ok(await _service.OpportunitiesMeta(filter));
    }

    /// <summary>
    /// Get one Opportunity
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Opportunity>> Opportunity(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Opportunity(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Opportunity
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateOpportunity(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId,
        [FromQuery()] OpportunityUpdateInput opportunityUpdateDto
    )
    {
        try
        {
            await _service.UpdateOpportunity(uniqueId, opportunityUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Activities records to Opportunity
    /// </summary>
    [HttpPost("{Id}/activities")]
    public async Task<ActionResult> ConnectActivities(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Activities records from Opportunity
    /// </summary>
    [HttpDelete("{Id}/activities")]
    public async Task<ActionResult> DisconnectActivities(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId,
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
    /// Find multiple Activities records for Opportunity
    /// </summary>
    [HttpGet("{Id}/activities")]
    public async Task<ActionResult<List<Activity>>> FindActivities(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId,
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
    /// Update multiple Activities records for Opportunity
    /// </summary>
    [HttpPatch("{Id}/activities")]
    public async Task<ActionResult> UpdateActivities(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId,
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
    /// Get a Customer record for Opportunity
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] OpportunityWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
