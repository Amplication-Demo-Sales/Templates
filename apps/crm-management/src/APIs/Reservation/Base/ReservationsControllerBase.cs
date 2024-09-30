using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ReservationsControllerBase : ControllerBase
{
    protected readonly IReservationsService _service;

    public ReservationsControllerBase(IReservationsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Reservation
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Reservation>> CreateReservation(ReservationCreateInput input)
    {
        var reservation = await _service.CreateReservation(input);

        return CreatedAtAction(nameof(Reservation), new { id = reservation.Id }, reservation);
    }

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteReservation(
        [FromRoute()] ReservationWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteReservation(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Reservations
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Reservation>>> Reservations(
        [FromQuery()] ReservationFindManyArgs filter
    )
    {
        return Ok(await _service.Reservations(filter));
    }

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ReservationsMeta(
        [FromQuery()] ReservationFindManyArgs filter
    )
    {
        return Ok(await _service.ReservationsMeta(filter));
    }

    /// <summary>
    /// Get one Reservation
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Reservation>> Reservation(
        [FromRoute()] ReservationWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Reservation(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Reservation
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateReservation(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromQuery()] ReservationUpdateInput reservationUpdateDto
    )
    {
        try
        {
            await _service.UpdateReservation(uniqueId, reservationUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Reservation
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] ReservationWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Connect multiple Payments records to Reservation
    /// </summary>
    [HttpPost("{Id}/payments")]
    public async Task<ActionResult> ConnectPayments(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromQuery()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.ConnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Reservation
    /// </summary>
    [HttpDelete("{Id}/payments")]
    public async Task<ActionResult> DisconnectPayments(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromBody()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.DisconnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Payments records for Reservation
    /// </summary>
    [HttpGet("{Id}/payments")]
    public async Task<ActionResult<List<Payment>>> FindPayments(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromQuery()] PaymentFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindPayments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Payments records for Reservation
    /// </summary>
    [HttpPatch("{Id}/payments")]
    public async Task<ActionResult> UpdatePayments(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromBody()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.UpdatePayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Room record for Reservation
    /// </summary>
    [HttpGet("{Id}/room")]
    public async Task<ActionResult<List<Room>>> GetRoom(
        [FromRoute()] ReservationWhereUniqueInput uniqueId
    )
    {
        var room = await _service.GetRoom(uniqueId);
        return Ok(room);
    }

    /// <summary>
    /// Connect multiple Services records to Reservation
    /// </summary>
    [HttpPost("{Id}/services")]
    public async Task<ActionResult> ConnectServices(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromQuery()] ServiceWhereUniqueInput[] servicesId
    )
    {
        try
        {
            await _service.ConnectServices(uniqueId, servicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Services records from Reservation
    /// </summary>
    [HttpDelete("{Id}/services")]
    public async Task<ActionResult> DisconnectServices(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromBody()] ServiceWhereUniqueInput[] servicesId
    )
    {
        try
        {
            await _service.DisconnectServices(uniqueId, servicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Services records for Reservation
    /// </summary>
    [HttpGet("{Id}/services")]
    public async Task<ActionResult<List<Service>>> FindServices(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromQuery()] ServiceFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindServices(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Services records for Reservation
    /// </summary>
    [HttpPatch("{Id}/services")]
    public async Task<ActionResult> UpdateServices(
        [FromRoute()] ReservationWhereUniqueInput uniqueId,
        [FromBody()] ServiceWhereUniqueInput[] servicesId
    )
    {
        try
        {
            await _service.UpdateServices(uniqueId, servicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
