using Microsoft.EntityFrameworkCore;
using ReservationManagement.APIs;
using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;
using ReservationManagement.APIs.Errors;
using ReservationManagement.APIs.Extensions;
using ReservationManagement.Infrastructure;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs;

public abstract class UsersServiceBase : IUsersService
{
    protected readonly ReservationManagementDbContext _context;

    public UsersServiceBase(ReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    public async Task<User> CreateUser(UserCreateInput createDto)
    {
        var user = new UserDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Password = createDto.Password,
            UpdatedAt = createDto.UpdatedAt,
            Username = createDto.Username
        };

        if (createDto.Id != null)
        {
            user.Id = createDto.Id;
        }
        if (createDto.Reservations != null)
        {
            user.Reservations = await _context
                .Reservations.Where(reservation =>
                    createDto.Reservations.Select(t => t.Id).Contains(reservation.Id)
                )
                .ToListAsync();
        }

        if (createDto.Reviews != null)
        {
            user.Reviews = await _context
                .Reviews.Where(review => createDto.Reviews.Select(t => t.Id).Contains(review.Id))
                .ToListAsync();
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserDbModel>(user.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    public async Task DeleteUser(UserWhereUniqueInput uniqueId)
    {
        var user = await _context.Users.FindAsync(uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    public async Task<List<User>> Users(UserFindManyArgs findManyArgs)
    {
        var users = await _context
            .Users.Include(x => x.Reviews)
            .Include(x => x.Reservations)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return users.ConvertAll(user => user.ToDto());
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public async Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs)
    {
        var count = await _context.Users.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one User
    /// </summary>
    public async Task<User> User(UserWhereUniqueInput uniqueId)
    {
        var users = await this.Users(
            new UserFindManyArgs { Where = new UserWhereInput { Id = uniqueId.Id } }
        );
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    /// <summary>
    /// Update one User
    /// </summary>
    public async Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto)
    {
        var user = updateDto.ToModel(uniqueId);

        if (updateDto.Reservations != null)
        {
            user.Reservations = await _context
                .Reservations.Where(reservation =>
                    updateDto.Reservations.Select(t => t).Contains(reservation.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Reviews != null)
        {
            user.Reviews = await _context
                .Reviews.Where(review => updateDto.Reviews.Select(t => t).Contains(review.Id))
                .ToListAsync();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Id == user.Id))
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
    /// Connect multiple Reservations records to User
    /// </summary>
    public async Task ConnectReservations(
        UserWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Reservations);

        foreach (var child in childrenToConnect)
        {
            parent.Reservations.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Reservations records from User
    /// </summary>
    public async Task DisconnectReservations(
        UserWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Reservations?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Reservations records for User
    /// </summary>
    public async Task<List<Reservation>> FindReservations(
        UserWhereUniqueInput uniqueId,
        ReservationFindManyArgs userFindManyArgs
    )
    {
        var reservations = await _context
            .Reservations.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return reservations.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reservations records for User
    /// </summary>
    public async Task UpdateReservations(
        UserWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.Reservations = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Reviews records to User
    /// </summary>
    public async Task ConnectReviews(
        UserWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Reviews);

        foreach (var child in childrenToConnect)
        {
            parent.Reviews.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Reviews records from User
    /// </summary>
    public async Task DisconnectReviews(
        UserWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Reviews?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Reviews records for User
    /// </summary>
    public async Task<List<Review>> FindReviews(
        UserWhereUniqueInput uniqueId,
        ReviewFindManyArgs userFindManyArgs
    )
    {
        var reviews = await _context
            .Reviews.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return reviews.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reviews records for User
    /// </summary>
    public async Task UpdateReviews(
        UserWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reviews.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.Reviews = children;
        await _context.SaveChangesAsync();
    }
}
