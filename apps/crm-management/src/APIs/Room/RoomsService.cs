using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(CrmManagementDbContext context)
        : base(context) { }
}
