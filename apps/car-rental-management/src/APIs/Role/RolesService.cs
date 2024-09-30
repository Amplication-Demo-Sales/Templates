using CarRentalManagementMobile.Infrastructure;

namespace CarRentalManagementMobile.APIs;

public class RolesService : RolesServiceBase
{
    public RolesService(CarRentalManagementMobileDbContext context)
        : base(context) { }
}
