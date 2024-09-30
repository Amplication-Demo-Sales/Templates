using ReservationManagement.Infrastructure;

namespace ReservationManagement.APIs;

public class ReservationsService : ReservationsServiceBase
{
    public ReservationsService(ReservationManagementDbContext context)
        : base(context) { }
}
