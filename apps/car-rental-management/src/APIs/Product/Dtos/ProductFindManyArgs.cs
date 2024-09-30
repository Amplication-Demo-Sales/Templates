using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagementMobile.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ProductFindManyArgs : FindManyInput<Product, ProductWhereInput> { }
