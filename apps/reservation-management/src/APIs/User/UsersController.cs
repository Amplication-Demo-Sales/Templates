using Microsoft.AspNetCore.Mvc;

namespace ReservationManagement.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
