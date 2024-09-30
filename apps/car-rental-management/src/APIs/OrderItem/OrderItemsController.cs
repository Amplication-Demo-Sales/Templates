using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs;

[ApiController()]
public class OrderItemsController : OrderItemsControllerBase
{
    public OrderItemsController(IOrderItemsService service)
        : base(service) { }
}
