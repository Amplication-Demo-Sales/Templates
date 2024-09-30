using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class OpportunitiesController : OpportunitiesControllerBase
{
    public OpportunitiesController(IOpportunitiesService service)
        : base(service) { }
}
