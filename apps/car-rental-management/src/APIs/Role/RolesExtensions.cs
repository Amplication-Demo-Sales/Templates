using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.Infrastructure.Models;

namespace CarRentalManagementMobile.APIs.Extensions;

public static class RolesExtensions
{
    public static Role ToDto(this RoleDbModel model)
    {
        return new Role
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
            Users = model.Users?.Select(x => x.Id).ToList(),
        };
    }

    public static RoleDbModel ToModel(this RoleUpdateInput updateDto, RoleWhereUniqueInput uniqueId)
    {
        var role = new RoleDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            role.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            role.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return role;
    }
}
