using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
