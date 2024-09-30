using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class ActivitiesController : ActivitiesControllerBase
{
    public ActivitiesController(IActivitiesService service)
        : base(service) { }
}
