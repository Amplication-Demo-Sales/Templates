using ReservationManagementMobile.Infrastructure;

namespace ReservationManagementMobile.APIs;

public class ReservationsService : ReservationsServiceBase
{
    public ReservationsService(ReservationManagementMobileDbContext context)
        : base(context) { }
}
