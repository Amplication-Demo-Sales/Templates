using ReservationManagement.APIs.Dtos;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs.Extensions;

public static class RoomsExtensions
{
    public static Room ToDto(this RoomDbModel model)
    {
        return new Room
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Price = model.Price,
            Reservations = model.Reservations?.Select(x => x.Id).ToList(),
            RoomNumber = model.RoomNumber,
            TypeField = model.TypeField,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RoomDbModel ToModel(this RoomUpdateInput updateDto, RoomWhereUniqueInput uniqueId)
    {
        var room = new RoomDbModel
        {
            Id = uniqueId.Id,
            Price = updateDto.Price,
            RoomNumber = updateDto.RoomNumber,
            TypeField = updateDto.TypeField
        };

        if (updateDto.CreatedAt != null)
        {
            room.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            room.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return room;
    }
}
