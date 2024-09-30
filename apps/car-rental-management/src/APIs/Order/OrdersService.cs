using CarRentalManagementMobile.Infrastructure;

namespace CarRentalManagementMobile.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(CarRentalManagementMobileDbContext context)
        : base(context) { }
}
