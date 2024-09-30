using Microsoft.AspNetCore.Mvc;

namespace ReservationManagement.APIs;

[ApiController()]
public class ReservationsController : ReservationsControllerBase
{
    public ReservationsController(IReservationsService service)
        : base(service) { }
}
