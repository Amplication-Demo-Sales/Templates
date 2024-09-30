using Microsoft.EntityFrameworkCore;
using ReservationManagement.APIs;
using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;
using ReservationManagement.APIs.Errors;
using ReservationManagement.APIs.Extensions;
using ReservationManagement.Infrastructure;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs;

public abstract class ReservationsServiceBase : IReservationsService
{
    protected readonly ReservationManagementDbContext _context;

    public ReservationsServiceBase(ReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Reservation
    /// </summary>
    public async Task<Reservation> CreateReservation(ReservationCreateInput createDto)
    {
        var reservation = new ReservationDbModel
        {
            CreatedAt = createDto.CreatedAt,
            EndDate = createDto.EndDate,
            ReservationDate = createDto.ReservationDate,
            StartDate = createDto.StartDate,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            reservation.Id = createDto.Id;
        }
        if (createDto.Payments != null)
        {
            reservation.Payments = await _context
                .Payments.Where(payment =>
                    createDto.Payments.Select(t => t.Id).Contains(payment.Id)
                )
                .ToListAsync();
        }

        if (createDto.Reviews != null)
        {
            reservation.Reviews = await _context
                .Reviews.Where(review => createDto.Reviews.Select(t => t.Id).Contains(review.Id))
                .ToListAsync();
        }

        if (createDto.Room != null)
        {
            reservation.Room = await _context
                .Rooms.Where(room => createDto.Room.Id == room.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.User != null)
        {
            reservation.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ReservationDbModel>(reservation.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    public async Task DeleteReservation(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context.Reservations.FindAsync(uniqueId.Id);
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Reservations
    /// </summary>
    public async Task<List<Reservation>> Reservations(ReservationFindManyArgs findManyArgs)
    {
        var reservations = await _context
            .Reservations.Include(x => x.Payments)
            .Include(x => x.Room)
            .Include(x => x.Reviews)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return reservations.ConvertAll(reservation => reservation.ToDto());
    }

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    public async Task<MetadataDto> ReservationsMeta(ReservationFindManyArgs findManyArgs)
    {
        var count = await _context.Reservations.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Reservation
    /// </summary>
    public async Task<Reservation> Reservation(ReservationWhereUniqueInput uniqueId)
    {
        var reservations = await this.Reservations(
            new ReservationFindManyArgs { Where = new ReservationWhereInput { Id = uniqueId.Id } }
        );
        var reservation = reservations.FirstOrDefault();
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        return reservation;
    }

    /// <summary>
    /// Update one Reservation
    /// </summary>
    public async Task UpdateReservation(
        ReservationWhereUniqueInput uniqueId,
        ReservationUpdateInput updateDto
    )
    {
        var reservation = updateDto.ToModel(uniqueId);

        if (updateDto.Payments != null)
        {
            reservation.Payments = await _context
                .Payments.Where(payment => updateDto.Payments.Select(t => t).Contains(payment.Id))
                .ToListAsync();
        }

        if (updateDto.Reviews != null)
        {
            reservation.Reviews = await _context
                .Reviews.Where(review => updateDto.Reviews.Select(t => t).Contains(review.Id))
                .ToListAsync();
        }

        if (updateDto.Room != null)
        {
            reservation.Room = await _context
                .Rooms.Where(room => updateDto.Room == room.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.User != null)
        {
            reservation.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(reservation).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reservations.Any(e => e.Id == reservation.Id))
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
    /// Connect multiple Payments records to Reservation
    /// </summary>
    public async Task ConnectPayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reservations.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Payments);

        foreach (var child in childrenToConnect)
        {
            parent.Payments.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Reservation
    /// </summary>
    public async Task DisconnectPayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reservations.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Payments?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Payments records for Reservation
    /// </summary>
    public async Task<List<Payment>> FindPayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentFindManyArgs reservationFindManyArgs
    )
    {
        var payments = await _context
            .Payments.Where(m => m.ReservationId == uniqueId.Id)
            .ApplyWhere(reservationFindManyArgs.Where)
            .ApplySkip(reservationFindManyArgs.Skip)
            .ApplyTake(reservationFindManyArgs.Take)
            .ApplyOrderBy(reservationFindManyArgs.SortBy)
            .ToListAsync();

        return payments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Payments records for Reservation
    /// </summary>
    public async Task UpdatePayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var reservation = await _context
            .Reservations.Include(t => t.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        reservation.Payments = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Reviews records to Reservation
    /// </summary>
    public async Task ConnectReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reservations.Include(x => x.Reviews)
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
    /// Disconnect multiple Reviews records from Reservation
    /// </summary>
    public async Task DisconnectReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reservations.Include(x => x.Reviews)
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
    /// Find multiple Reviews records for Reservation
    /// </summary>
    public async Task<List<Review>> FindReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewFindManyArgs reservationFindManyArgs
    )
    {
        var reviews = await _context
            .Reviews.Where(m => m.ReservationId == uniqueId.Id)
            .ApplyWhere(reservationFindManyArgs.Where)
            .ApplySkip(reservationFindManyArgs.Skip)
            .ApplyTake(reservationFindManyArgs.Take)
            .ApplyOrderBy(reservationFindManyArgs.SortBy)
            .ToListAsync();

        return reviews.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reviews records for Reservation
    /// </summary>
    public async Task UpdateReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] childrenIds
    )
    {
        var reservation = await _context
            .Reservations.Include(t => t.Reviews)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (reservation == null)
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

        reservation.Reviews = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Room record for Reservation
    /// </summary>
    public async Task<Room> GetRoom(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context
            .Reservations.Where(reservation => reservation.Id == uniqueId.Id)
            .Include(reservation => reservation.Room)
            .FirstOrDefaultAsync();
        if (reservation == null)
        {
            throw new NotFoundException();
        }
        return reservation.Room.ToDto();
    }

    /// <summary>
    /// Get a User record for Reservation
    /// </summary>
    public async Task<User> GetUser(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context
            .Reservations.Where(reservation => reservation.Id == uniqueId.Id)
            .Include(reservation => reservation.User)
            .FirstOrDefaultAsync();
        if (reservation == null)
        {
            throw new NotFoundException();
        }
        return reservation.User.ToDto();
    }
}
