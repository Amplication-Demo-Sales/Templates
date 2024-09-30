using CarRentalManagementMobile.APIs;
using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.APIs.Errors;
using CarRentalManagementMobile.APIs.Extensions;
using CarRentalManagementMobile.Infrastructure;
using CarRentalManagementMobile.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementMobile.APIs;

public abstract class RolesServiceBase : IRolesService
{
    protected readonly CarRentalManagementMobileDbContext _context;

    public RolesServiceBase(CarRentalManagementMobileDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Role
    /// </summary>
    public async Task<Role> CreateRole(RoleCreateInput createDto)
    {
        var role = new RoleDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            role.Id = createDto.Id;
        }
        if (createDto.Users != null)
        {
            role.Users = await _context
                .Users.Where(user => createDto.Users.Select(t => t.Id).Contains(user.Id))
                .ToListAsync();
        }

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RoleDbModel>(role.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Role
    /// </summary>
    public async Task DeleteRole(RoleWhereUniqueInput uniqueId)
    {
        var role = await _context.Roles.FindAsync(uniqueId.Id);
        if (role == null)
        {
            throw new NotFoundException();
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Roles
    /// </summary>
    public async Task<List<Role>> Roles(RoleFindManyArgs findManyArgs)
    {
        var roles = await _context
            .Roles.Include(x => x.Users)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return roles.ConvertAll(role => role.ToDto());
    }

    /// <summary>
    /// Meta data about Role records
    /// </summary>
    public async Task<MetadataDto> RolesMeta(RoleFindManyArgs findManyArgs)
    {
        var count = await _context.Roles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Role
    /// </summary>
    public async Task<Role> Role(RoleWhereUniqueInput uniqueId)
    {
        var roles = await this.Roles(
            new RoleFindManyArgs { Where = new RoleWhereInput { Id = uniqueId.Id } }
        );
        var role = roles.FirstOrDefault();
        if (role == null)
        {
            throw new NotFoundException();
        }

        return role;
    }

    /// <summary>
    /// Update one Role
    /// </summary>
    public async Task UpdateRole(RoleWhereUniqueInput uniqueId, RoleUpdateInput updateDto)
    {
        var role = updateDto.ToModel(uniqueId);

        if (updateDto.Users != null)
        {
            role.Users = await _context
                .Users.Where(user => updateDto.Users.Select(t => t).Contains(user.Id))
                .ToListAsync();
        }

        _context.Entry(role).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Roles.Any(e => e.Id == role.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Users records to Role
    /// </summary>
    public async Task ConnectUsers(
        RoleWhereUniqueInput uniqueId,
        UserWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Roles.Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Users.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Users);

        foreach (var child in childrenToConnect)
        {
            parent.Users.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Users records from Role
    /// </summary>
    public async Task DisconnectUsers(
        RoleWhereUniqueInput uniqueId,
        UserWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Roles.Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Users.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Users?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Users records for Role
    /// </summary>
    public async Task<List<User>> FindUsers(
        RoleWhereUniqueInput uniqueId,
        UserFindManyArgs roleFindManyArgs
    )
    {
        var users = await _context
            .Users.Where(m => m.RoleId == uniqueId.Id)
            .ApplyWhere(roleFindManyArgs.Where)
            .ApplySkip(roleFindManyArgs.Skip)
            .ApplyTake(roleFindManyArgs.Take)
            .ApplyOrderBy(roleFindManyArgs.SortBy)
            .ToListAsync();

        return users.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Users records for Role
    /// </summary>
    public async Task UpdateUsers(RoleWhereUniqueInput uniqueId, UserWhereUniqueInput[] childrenIds)
    {
        var role = await _context
            .Roles.Include(t => t.Users)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (role == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Users.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        role.Users = children;
        await _context.SaveChangesAsync();
    }
}
