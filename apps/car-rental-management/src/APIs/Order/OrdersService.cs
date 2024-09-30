using CarRentalManagement.Infrastructure;

namespace CarRentalManagement.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(CarRentalManagementDbContext context)
        : base(context) { }
}
