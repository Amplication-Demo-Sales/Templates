using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class ReservationsServiceBase : IReservationsService
{
    protected readonly CrmManagementDbContext _context;

    public ReservationsServiceBase(CrmManagementDbContext context)
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
            NumberOfGuests = createDto.NumberOfGuests,
            ReservationDate = createDto.ReservationDate,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            reservation.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            reservation.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Payments != null)
        {
            reservation.Payments = await _context
                .Payments.Where(payment =>
                    createDto.Payments.Select(t => t.Id).Contains(payment.Id)
                )
                .ToListAsync();
        }

        if (createDto.Room != null)
        {
            reservation.Room = await _context
                .Rooms.Where(room => createDto.Room.Id == room.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Services != null)
        {
            reservation.Services = await _context
                .Services.Where(service =>
                    createDto.Services.Select(t => t.Id).Contains(service.Id)
                )
                .ToListAsync();
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
            .Reservations.Include(x => x.Customer)
            .Include(x => x.Room)
            .Include(x => x.Services)
            .Include(x => x.Payments)
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

        if (updateDto.Customer != null)
        {
            reservation.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Payments != null)
        {
            reservation.Payments = await _context
                .Payments.Where(payment => updateDto.Payments.Select(t => t).Contains(payment.Id))
                .ToListAsync();
        }

        if (updateDto.Room != null)
        {
            reservation.Room = await _context
                .Rooms.Where(room => updateDto.Room == room.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Services != null)
        {
            reservation.Services = await _context
                .Services.Where(service => updateDto.Services.Select(t => t).Contains(service.Id))
                .ToListAsync();
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
    /// Get a Customer record for Reservation
    /// </summary>
    public async Task<Customer> GetCustomer(ReservationWhereUniqueInput uniqueId)
    {
        var reservation = await _context
            .Reservations.Where(reservation => reservation.Id == uniqueId.Id)
            .Include(reservation => reservation.Customer)
            .FirstOrDefaultAsync();
        if (reservation == null)
        {
            throw new NotFoundException();
        }
        return reservation.Customer.ToDto();
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
    /// Connect multiple Services records to Reservation
    /// </summary>
    public async Task ConnectServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reservations.Include(x => x.Services)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Services.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Services);

        foreach (var child in childrenToConnect)
        {
            parent.Services.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Services records from Reservation
    /// </summary>
    public async Task DisconnectServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reservations.Include(x => x.Services)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Services.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Services?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Services records for Reservation
    /// </summary>
    public async Task<List<Service>> FindServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceFindManyArgs reservationFindManyArgs
    )
    {
        var services = await _context
            .Services.Where(m => m.ReservationId == uniqueId.Id)
            .ApplyWhere(reservationFindManyArgs.Where)
            .ApplySkip(reservationFindManyArgs.Skip)
            .ApplyTake(reservationFindManyArgs.Take)
            .ApplyOrderBy(reservationFindManyArgs.SortBy)
            .ToListAsync();

        return services.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Services records for Reservation
    /// </summary>
    public async Task UpdateServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceWhereUniqueInput[] childrenIds
    )
    {
        var reservation = await _context
            .Reservations.Include(t => t.Services)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (reservation == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Services.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        reservation.Services = children;
        await _context.SaveChangesAsync();
    }
}
