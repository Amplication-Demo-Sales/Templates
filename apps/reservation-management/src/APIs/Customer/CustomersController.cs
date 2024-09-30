using Microsoft.AspNetCore.Mvc;

namespace ReservationManagementMobile.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
