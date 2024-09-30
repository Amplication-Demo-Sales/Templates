using ReservationManagement.Infrastructure;

namespace ReservationManagement.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(ReservationManagementDbContext context)
        : base(context) { }
}
