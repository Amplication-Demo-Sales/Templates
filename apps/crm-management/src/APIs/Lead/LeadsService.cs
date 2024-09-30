using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class LeadsService : LeadsServiceBase
{
    public LeadsService(CrmManagementDbContext context)
        : base(context) { }
}
