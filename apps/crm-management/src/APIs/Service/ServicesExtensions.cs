using CrmManagement.APIs.Dtos;
using CrmManagement.Infrastructure.Models;

namespace CrmManagement.APIs.Extensions;

public static class ServicesExtensions
{
    public static Service ToDto(this ServiceDbModel model)
    {
        return new Service
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Price = model.Price,
            Reservation = model.ReservationId,
            ServiceName = model.ServiceName,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ServiceDbModel ToModel(
        this ServiceUpdateInput updateDto,
        ServiceWhereUniqueInput uniqueId
    )
    {
        var service = new ServiceDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Price = updateDto.Price,
            ServiceName = updateDto.ServiceName
        };

        if (updateDto.CreatedAt != null)
        {
            service.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Reservation != null)
        {
            service.ReservationId = updateDto.Reservation;
        }
        if (updateDto.UpdatedAt != null)
        {
            service.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return service;
    }
}
