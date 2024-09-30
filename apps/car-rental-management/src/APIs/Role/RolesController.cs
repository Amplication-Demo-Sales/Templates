using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs;

[ApiController()]
public class RolesController : RolesControllerBase
{
    public RolesController(IRolesService service)
        : base(service) { }
}
