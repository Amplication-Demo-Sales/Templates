using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.Infrastructure.Models;

namespace CarRentalManagementMobile.APIs.Extensions;

public static class UsersExtensions
{
    public static User ToDto(this UserDbModel model)
    {
        return new User
        {
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            Id = model.Id,
            Password = model.Password,
            Role = model.RoleId,
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
            Password = updateDto.Password,
            Username = updateDto.Username
        };

        if (updateDto.CreatedAt != null)
        {
            user.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Role != null)
        {
            user.RoleId = updateDto.Role;
        }
        if (updateDto.UpdatedAt != null)
        {
            user.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return user;
    }
}
