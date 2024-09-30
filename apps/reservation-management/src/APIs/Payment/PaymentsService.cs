using ReservationManagement.Infrastructure;

namespace ReservationManagement.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(ReservationManagementDbContext context)
        : base(context) { }
}
