using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Activities records to Customer
    /// </summary>
    [HttpPost("{Id}/activities")]
    public async Task<ActionResult> ConnectActivities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Activities records from Customer
    /// </summary>
    [HttpDelete("{Id}/activities")]
    public async Task<ActionResult> DisconnectActivities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Find multiple Activities records for Customer
    /// </summary>
    [HttpGet("{Id}/activities")]
    public async Task<ActionResult<List<Activity>>> FindActivities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Update multiple Activities records for Customer
    /// </summary>
    [HttpPatch("{Id}/activities")]
    public async Task<ActionResult> UpdateActivities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
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
    /// Connect multiple Contacts records to Customer
    /// </summary>
    [HttpPost("{Id}/contacts")]
    public async Task<ActionResult> ConnectContacts(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ContactWhereUniqueInput[] contactsId
    )
    {
        try
        {
            await _service.ConnectContacts(uniqueId, contactsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Contacts records from Customer
    /// </summary>
    [HttpDelete("{Id}/contacts")]
    public async Task<ActionResult> DisconnectContacts(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ContactWhereUniqueInput[] contactsId
    )
    {
        try
        {
            await _service.DisconnectContacts(uniqueId, contactsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Contacts records for Customer
    /// </summary>
    [HttpGet("{Id}/contacts")]
    public async Task<ActionResult<List<Contact>>> FindContacts(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ContactFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindContacts(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Contacts records for Customer
    /// </summary>
    [HttpPatch("{Id}/contacts")]
    public async Task<ActionResult> UpdateContacts(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ContactWhereUniqueInput[] contactsId
    )
    {
        try
        {
            await _service.UpdateContacts(uniqueId, contactsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Leads records to Customer
    /// </summary>
    [HttpPost("{Id}/leads")]
    public async Task<ActionResult> ConnectLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] LeadWhereUniqueInput[] leadsId
    )
    {
        try
        {
            await _service.ConnectLeads(uniqueId, leadsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Leads records from Customer
    /// </summary>
    [HttpDelete("{Id}/leads")]
    public async Task<ActionResult> DisconnectLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] LeadWhereUniqueInput[] leadsId
    )
    {
        try
        {
            await _service.DisconnectLeads(uniqueId, leadsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Leads records for Customer
    /// </summary>
    [HttpGet("{Id}/leads")]
    public async Task<ActionResult<List<Lead>>> FindLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] LeadFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindLeads(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Leads records for Customer
    /// </summary>
    [HttpPatch("{Id}/leads")]
    public async Task<ActionResult> UpdateLeads(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] LeadWhereUniqueInput[] leadsId
    )
    {
        try
        {
            await _service.UpdateLeads(uniqueId, leadsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Opportunities records to Customer
    /// </summary>
    [HttpPost("{Id}/opportunities")]
    public async Task<ActionResult> ConnectOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OpportunityWhereUniqueInput[] opportunitiesId
    )
    {
        try
        {
            await _service.ConnectOpportunities(uniqueId, opportunitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Opportunities records from Customer
    /// </summary>
    [HttpDelete("{Id}/opportunities")]
    public async Task<ActionResult> DisconnectOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OpportunityWhereUniqueInput[] opportunitiesId
    )
    {
        try
        {
            await _service.DisconnectOpportunities(uniqueId, opportunitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Opportunities records for Customer
    /// </summary>
    [HttpGet("{Id}/opportunities")]
    public async Task<ActionResult<List<Opportunity>>> FindOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] OpportunityFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOpportunities(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Opportunities records for Customer
    /// </summary>
    [HttpPatch("{Id}/opportunities")]
    public async Task<ActionResult> UpdateOpportunities(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] OpportunityWhereUniqueInput[] opportunitiesId
    )
    {
        try
        {
            await _service.UpdateOpportunities(uniqueId, opportunitiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Reservations records to Customer
    /// </summary>
    [HttpPost("{Id}/reservations")]
    public async Task<ActionResult> ConnectReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ReservationWhereUniqueInput[] reservationsId
    )
    {
        try
        {
            await _service.ConnectReservations(uniqueId, reservationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Reservations records from Customer
    /// </summary>
    [HttpDelete("{Id}/reservations")]
    public async Task<ActionResult> DisconnectReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ReservationWhereUniqueInput[] reservationsId
    )
    {
        try
        {
            await _service.DisconnectReservations(uniqueId, reservationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Reservations records for Customer
    /// </summary>
    [HttpGet("{Id}/reservations")]
    public async Task<ActionResult<List<Reservation>>> FindReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ReservationFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindReservations(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Reservations records for Customer
    /// </summary>
    [HttpPatch("{Id}/reservations")]
    public async Task<ActionResult> UpdateReservations(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ReservationWhereUniqueInput[] reservationsId
    )
    {
        try
        {
            await _service.UpdateReservations(uniqueId, reservationsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
