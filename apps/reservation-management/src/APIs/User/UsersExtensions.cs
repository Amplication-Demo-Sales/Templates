using ReservationManagement.APIs.Dtos;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs.Extensions;

public static class UsersExtensions
{
    public static User ToDto(this UserDbModel model)
    {
        return new User
        {
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            Password = model.Password,
            Reservations = model.Reservations?.Select(x => x.Id).ToList(),
            Reviews = model.Reviews?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
            Username = model.Username,
        };
    }

    public static UserDbModel ToModel(this UserUpdateInput updateDto, UserWhereUniqueInput uniqueId)
    {
        var user = new UserDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Password = updateDto.Password,
            Username = updateDto.Username
        };

        if (updateDto.CreatedAt != null)
        {
            user.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            user.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return user;
    }
}
