using CarRentalManagement.Infrastructure;

namespace CarRentalManagement.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(CarRentalManagementDbContext context)
        : base(context) { }
}
