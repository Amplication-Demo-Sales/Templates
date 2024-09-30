using Microsoft.AspNetCore.Mvc;

namespace ReservationManagementMobile.APIs;

[ApiController()]
public class RoomsController : RoomsControllerBase
{
    public RoomsController(IRoomsService service)
        : base(service) { }
}
