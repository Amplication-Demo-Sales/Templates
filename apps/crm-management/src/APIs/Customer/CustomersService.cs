using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(CrmManagementDbContext context)
        : base(context) { }
}
