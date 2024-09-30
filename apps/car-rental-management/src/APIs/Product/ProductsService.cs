using CarRentalManagement.Infrastructure;

namespace CarRentalManagement.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(CarRentalManagementDbContext context)
        : base(context) { }
}
