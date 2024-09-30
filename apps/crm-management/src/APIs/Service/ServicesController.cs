using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class ServicesController : ServicesControllerBase
{
    public ServicesController(IServicesService service)
        : base(service) { }
}
