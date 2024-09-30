using ReservationManagement.Infrastructure;

namespace ReservationManagement.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(ReservationManagementDbContext context)
        : base(context) { }
}
