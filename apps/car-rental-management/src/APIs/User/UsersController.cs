using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
