using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.APIs.Dtos;

namespace CarRentalManagementMobile.APIs;

public interface IRolesService
{
    /// <summary>
    /// Create one Role
    /// </summary>
    public Task<Role> CreateRole(RoleCreateInput role);

    /// <summary>
    /// Delete one Role
    /// </summary>
    public Task DeleteRole(RoleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Roles
    /// </summary>
    public Task<List<Role>> Roles(RoleFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Role records
    /// </summary>
    public Task<MetadataDto> RolesMeta(RoleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Role
    /// </summary>
    public Task<Role> Role(RoleWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Role
    /// </summary>
    public Task UpdateRole(RoleWhereUniqueInput uniqueId, RoleUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Users records to Role
    /// </summary>
    public Task ConnectUsers(RoleWhereUniqueInput uniqueId, UserWhereUniqueInput[] usersId);

    /// <summary>
    /// Disconnect multiple Users records from Role
    /// </summary>
    public Task DisconnectUsers(RoleWhereUniqueInput uniqueId, UserWhereUniqueInput[] usersId);

    /// <summary>
    /// Find multiple Users records for Role
    /// </summary>
    public Task<List<User>> FindUsers(
        RoleWhereUniqueInput uniqueId,
        UserFindManyArgs UserFindManyArgs
    );

    /// <summary>
    /// Update multiple Users records for Role
    /// </summary>
    public Task UpdateUsers(RoleWhereUniqueInput uniqueId, UserWhereUniqueInput[] usersId);
}
