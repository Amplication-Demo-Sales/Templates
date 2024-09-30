using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class RoomsController : RoomsControllerBase
{
    public RoomsController(IRoomsService service)
        : base(service) { }
}
