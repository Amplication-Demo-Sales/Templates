using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
