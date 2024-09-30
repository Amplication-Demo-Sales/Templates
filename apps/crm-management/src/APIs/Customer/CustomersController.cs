using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
