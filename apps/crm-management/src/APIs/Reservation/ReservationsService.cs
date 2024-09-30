using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class ReservationsService : ReservationsServiceBase
{
    public ReservationsService(CrmManagementDbContext context)
        : base(context) { }
}
