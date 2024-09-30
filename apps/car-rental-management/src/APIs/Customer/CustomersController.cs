using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
