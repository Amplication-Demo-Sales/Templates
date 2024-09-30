using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class ActivitiesService : ActivitiesServiceBase
{
    public ActivitiesService(CrmManagementDbContext context)
        : base(context) { }
}
