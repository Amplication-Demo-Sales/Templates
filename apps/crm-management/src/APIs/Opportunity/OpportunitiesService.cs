using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class OpportunitiesService : OpportunitiesServiceBase
{
    public OpportunitiesService(CrmManagementDbContext context)
        : base(context) { }
}
