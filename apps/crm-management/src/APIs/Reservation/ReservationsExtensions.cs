using CrmManagement.APIs.Dtos;
using CrmManagement.Infrastructure.Models;

namespace CrmManagement.APIs.Extensions;

public static class ReservationsExtensions
{
    public static Reservation ToDto(this ReservationDbModel model)
    {
        return new Reservation
        {
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            NumberOfGuests = model.NumberOfGuests,
            Payments = model.Payments?.Select(x => x.Id).ToList(),
            ReservationDate = model.ReservationDate,
            Room = model.RoomId,
            Services = model.Services?.Select(x => x.Id).ToList(),
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ReservationDbModel ToModel(
        this ReservationUpdateInput updateDto,
        ReservationWhereUniqueInput uniqueId
    )
    {
        var reservation = new ReservationDbModel
        {
            Id = uniqueId.Id,
            NumberOfGuests = updateDto.NumberOfGuests,
            ReservationDate = updateDto.ReservationDate,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            reservation.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            reservation.CustomerId = updateDto.Customer;
        }
        if (updateDto.Room != null)
        {
            reservation.RoomId = updateDto.Room;
        }
        if (updateDto.UpdatedAt != null)
        {
            reservation.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return reservation;
    }
}
