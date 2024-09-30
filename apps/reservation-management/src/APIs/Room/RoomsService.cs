using ReservationManagementMobile.Infrastructure;

namespace ReservationManagementMobile.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(ReservationManagementMobileDbContext context)
        : base(context) { }
}
