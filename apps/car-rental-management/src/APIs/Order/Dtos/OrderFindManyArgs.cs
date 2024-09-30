using CarRentalManagement.APIs.Common;
using CarRentalManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class OrderFindManyArgs : FindManyInput<Order, OrderWhereInput> { }
