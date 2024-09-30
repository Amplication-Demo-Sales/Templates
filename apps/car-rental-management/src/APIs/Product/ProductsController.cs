using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
