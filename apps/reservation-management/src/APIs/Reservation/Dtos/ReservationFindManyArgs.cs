using Microsoft.AspNetCore.Mvc;
using ReservationManagement.APIs.Common;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ReservationFindManyArgs : FindManyInput<Reservation, ReservationWhereInput> { }
