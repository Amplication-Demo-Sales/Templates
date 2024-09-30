using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class ServicesServiceBase : IServicesService
{
    protected readonly CrmManagementDbContext _context;

    public ServicesServiceBase(CrmManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Service
    /// </summary>
    public async Task<Service> CreateService(ServiceCreateInput createDto)
    {
        var service = new ServiceDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Price = createDto.Price,
            ServiceName = createDto.ServiceName,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            service.Id = createDto.Id;
        }
        if (createDto.Reservation != null)
        {
            service.Reservation = await _context
                .Reservations.Where(reservation => createDto.Reservation.Id == reservation.Id)
                .FirstOrDefaultAsync();
        }

        _context.Services.Add(service);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ServiceDbModel>(service.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Service
    /// </summary>
    public async Task DeleteService(ServiceWhereUniqueInput uniqueId)
    {
        var service = await _context.Services.FindAsync(uniqueId.Id);
        if (service == null)
        {
            throw new NotFoundException();
        }

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Services
    /// </summary>
    public async Task<List<Service>> Services(ServiceFindManyArgs findManyArgs)
    {
        var services = await _context
            .Services.Include(x => x.Reservation)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return services.ConvertAll(service => service.ToDto());
    }

    /// <summary>
    /// Meta data about Service records
    /// </summary>
    public async Task<MetadataDto> ServicesMeta(ServiceFindManyArgs findManyArgs)
    {
        var count = await _context.Services.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Service
    /// </summary>
    public async Task<Service> Service(ServiceWhereUniqueInput uniqueId)
    {
        var services = await this.Services(
            new ServiceFindManyArgs { Where = new ServiceWhereInput { Id = uniqueId.Id } }
        );
        var service = services.FirstOrDefault();
        if (service == null)
        {
            throw new NotFoundException();
        }

        return service;
    }

    /// <summary>
    /// Update one Service
    /// </summary>
    public async Task UpdateService(ServiceWhereUniqueInput uniqueId, ServiceUpdateInput updateDto)
    {
        var service = updateDto.ToModel(uniqueId);

        if (updateDto.Reservation != null)
        {
            service.Reservation = await _context
                .Reservations.Where(reservation => updateDto.Reservation == reservation.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(service).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Services.Any(e => e.Id == service.Id))
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
    /// Get a Reservation record for Service
    /// </summary>
    public async Task<Reservation> GetReservation(ServiceWhereUniqueInput uniqueId)
    {
        var service = await _context
            .Services.Where(service => service.Id == uniqueId.Id)
            .Include(service => service.Reservation)
            .FirstOrDefaultAsync();
        if (service == null)
        {
            throw new NotFoundException();
        }
        return service.Reservation.ToDto();
    }
}
