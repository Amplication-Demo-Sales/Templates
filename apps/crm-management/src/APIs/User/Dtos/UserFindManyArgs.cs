using CrmManagement.APIs.Common;
using CrmManagement.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrmManagement.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserFindManyArgs : FindManyInput<User, UserWhereInput> { }
