using ReservationManagement.APIs.Dtos;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs.Extensions;

public static class ReservationsExtensions
{
    public static Reservation ToDto(this ReservationDbModel model)
    {
        return new Reservation
        {
            CreatedAt = model.CreatedAt,
            EndDate = model.EndDate,
            Id = model.Id,
            Payments = model.Payments?.Select(x => x.Id).ToList(),
            ReservationDate = model.ReservationDate,
            Reviews = model.Reviews?.Select(x => x.Id).ToList(),
            Room = model.RoomId,
            StartDate = model.StartDate,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
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
            EndDate = updateDto.EndDate,
            ReservationDate = updateDto.ReservationDate,
            StartDate = updateDto.StartDate,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            reservation.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Room != null)
        {
            reservation.RoomId = updateDto.Room;
        }
        if (updateDto.UpdatedAt != null)
        {
            reservation.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            reservation.UserId = updateDto.User;
        }

        return reservation;
    }
}
