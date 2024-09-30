using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(CrmManagementDbContext context)
        : base(context) { }
}
