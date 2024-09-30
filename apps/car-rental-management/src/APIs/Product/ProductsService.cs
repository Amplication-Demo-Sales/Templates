using CarRentalManagementMobile.Infrastructure;

namespace CarRentalManagementMobile.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(CarRentalManagementMobileDbContext context)
        : base(context) { }
}
