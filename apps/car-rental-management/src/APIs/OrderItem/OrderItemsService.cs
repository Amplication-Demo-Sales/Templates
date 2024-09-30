using CarRentalManagementMobile.Infrastructure;

namespace CarRentalManagementMobile.APIs;

public class OrderItemsService : OrderItemsServiceBase
{
    public OrderItemsService(CarRentalManagementMobileDbContext context)
        : base(context) { }
}
