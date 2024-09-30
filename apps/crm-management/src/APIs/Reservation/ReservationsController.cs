using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class ReservationsController : ReservationsControllerBase
{
    public ReservationsController(IReservationsService service)
        : base(service) { }
}
