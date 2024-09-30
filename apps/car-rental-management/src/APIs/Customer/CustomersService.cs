using CarRentalManagement.Infrastructure;

namespace CarRentalManagement.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(CarRentalManagementDbContext context)
        : base(context) { }
}
