using CrmManagement.APIs.Common;
using CrmManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class PaymentFindManyArgs : FindManyInput<Payment, PaymentWhereInput> { }
