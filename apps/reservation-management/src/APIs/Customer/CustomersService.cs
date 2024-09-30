using ReservationManagementMobile.Infrastructure;

namespace ReservationManagementMobile.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(ReservationManagementMobileDbContext context)
        : base(context) { }
}
