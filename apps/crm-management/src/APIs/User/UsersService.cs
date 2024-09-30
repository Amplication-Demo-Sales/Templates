using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(CrmManagementDbContext context)
        : base(context) { }
}
