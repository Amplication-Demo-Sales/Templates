using Microsoft.AspNetCore.Mvc;
using ReservationManagementMobile.APIs.Common;
using ReservationManagementMobile.Infrastructure.Models;

namespace ReservationManagementMobile.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class RoomFindManyArgs : FindManyInput<Room, RoomWhereInput> { }
