using CarRentalManagementMobile.APIs;
using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RolesControllerBase : ControllerBase
{
    protected readonly IRolesService _service;

    public RolesControllerBase(IRolesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Role
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Role>> CreateRole(RoleCreateInput input)
    {
        var role = await _service.CreateRole(input);

        return CreatedAtAction(nameof(Role), new { id = role.Id }, role);
    }

    /// <summary>
    /// Delete one Role
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteRole([FromRoute()] RoleWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRole(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Roles
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Role>>> Roles([FromQuery()] RoleFindManyArgs filter)
    {
        return Ok(await _service.Roles(filter));
    }

    /// <summary>
    /// Meta data about Role records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RolesMeta([FromQuery()] RoleFindManyArgs filter)
    {
        return Ok(await _service.RolesMeta(filter));
    }

    /// <summary>
    /// Get one Role
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Role>> Role([FromRoute()] RoleWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Role(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Role
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateRole(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromQuery()] RoleUpdateInput roleUpdateDto
    )
    {
        try
        {
            await _service.UpdateRole(uniqueId, roleUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Users records to Role
    /// </summary>
    [HttpPost("{Id}/users")]
    public async Task<ActionResult> ConnectUsers(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromQuery()] UserWhereUniqueInput[] usersId
    )
    {
        try
        {
            await _service.ConnectUsers(uniqueId, usersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Users records from Role
    /// </summary>
    [HttpDelete("{Id}/users")]
    public async Task<ActionResult> DisconnectUsers(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromBody()] UserWhereUniqueInput[] usersId
    )
    {
        try
        {
            await _service.DisconnectUsers(uniqueId, usersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Users records for Role
    /// </summary>
    [HttpGet("{Id}/users")]
    public async Task<ActionResult<List<User>>> FindUsers(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromQuery()] UserFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindUsers(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Users records for Role
    /// </summary>
    [HttpPatch("{Id}/users")]
    public async Task<ActionResult> UpdateUsers(
        [FromRoute()] RoleWhereUniqueInput uniqueId,
        [FromBody()] UserWhereUniqueInput[] usersId
    )
    {
        try
        {
            await _service.UpdateUsers(uniqueId, usersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
