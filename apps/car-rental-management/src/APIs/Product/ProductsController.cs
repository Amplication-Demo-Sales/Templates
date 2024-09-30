using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
