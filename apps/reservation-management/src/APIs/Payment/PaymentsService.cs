using ReservationManagementMobile.Infrastructure;

namespace ReservationManagementMobile.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(ReservationManagementMobileDbContext context)
        : base(context) { }
}
