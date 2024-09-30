using CrmManagement.Infrastructure;

namespace CrmManagement.APIs;

public class ContactsService : ContactsServiceBase
{
    public ContactsService(CrmManagementDbContext context)
        : base(context) { }
}
