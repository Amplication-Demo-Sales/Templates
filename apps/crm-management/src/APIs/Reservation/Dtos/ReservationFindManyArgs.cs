using CrmManagement.APIs.Common;
using CrmManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ReservationFindManyArgs : FindManyInput<Reservation, ReservationWhereInput> { }
