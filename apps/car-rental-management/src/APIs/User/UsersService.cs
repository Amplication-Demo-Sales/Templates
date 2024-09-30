using CarRentalManagementMobile.Infrastructure;

namespace CarRentalManagementMobile.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(CarRentalManagementMobileDbContext context)
        : base(context) { }
}
