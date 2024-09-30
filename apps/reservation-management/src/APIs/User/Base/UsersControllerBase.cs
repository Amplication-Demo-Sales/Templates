using Microsoft.AspNetCore.Mvc;
using ReservationManagement.APIs;
using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;
using ReservationManagement.APIs.Errors;

namespace ReservationManagement.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<User>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteUser(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<User>>> Users([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UsersMeta([FromQuery()] UserFindManyArgs filter)
    {
        return Ok(await _service.UsersMeta(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<User>> User([FromRoute()] UserWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.User(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one User
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(uniqueId, userUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Reservations records to User
    /// </summary>
    [HttpPost("{Id}/reservations")]
    public async Task<ActionResult> ConnectReservations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Reservations records from User
    /// </summary>
    [HttpDelete("{Id}/reservations")]
    public async Task<ActionResult> DisconnectReservations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Find multiple Reservations records for User
    /// </summary>
    [HttpGet("{Id}/reservations")]
    public async Task<ActionResult<List<Reservation>>> FindReservations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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
    /// Update multiple Reservations records for User
    /// </summary>
    [HttpPatch("{Id}/reservations")]
    public async Task<ActionResult> UpdateReservations(
        [FromRoute()] UserWhereUniqueInput uniqueId,
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

    /// <summary>
    /// Connect multiple Reviews records to User
    /// </summary>
    [HttpPost("{Id}/reviews")]
    public async Task<ActionResult> ConnectReviews(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.ConnectReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Reviews records from User
    /// </summary>
    [HttpDelete("{Id}/reviews")]
    public async Task<ActionResult> DisconnectReviews(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.DisconnectReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Reviews records for User
    /// </summary>
    [HttpGet("{Id}/reviews")]
    public async Task<ActionResult<List<Review>>> FindReviews(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromQuery()] ReviewFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindReviews(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Reviews records for User
    /// </summary>
    [HttpPatch("{Id}/reviews")]
    public async Task<ActionResult> UpdateReviews(
        [FromRoute()] UserWhereUniqueInput uniqueId,
        [FromBody()] ReviewWhereUniqueInput[] reviewsId
    )
    {
        try
        {
            await _service.UpdateReviews(uniqueId, reviewsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
