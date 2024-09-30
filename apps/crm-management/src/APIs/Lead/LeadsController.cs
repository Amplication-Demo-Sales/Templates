using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class LeadsController : LeadsControllerBase
{
    public LeadsController(ILeadsService service)
        : base(service) { }
}
