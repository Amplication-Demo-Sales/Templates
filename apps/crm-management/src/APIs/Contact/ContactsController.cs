using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs;

[ApiController()]
public class ContactsController : ContactsControllerBase
{
    public ContactsController(IContactsService service)
        : base(service) { }
}
