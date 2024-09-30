using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class ServicesService : ServicesServiceBase
{
    public ServicesService(CrmManagementDbContext context)
        : base(context) { }
}
