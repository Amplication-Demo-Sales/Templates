using CarRentalManagementMobile.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementMobile.Infrastructure;

public class CarRentalManagementMobileDbContext : DbContext
{
    public CarRentalManagementMobileDbContext(
        DbContextOptions<CarRentalManagementMobileDbContext> options
    )
        : base(options) { }

    public DbSet<OrderItemDbModel> OrderItems { get; set; }

    public DbSet<RoleDbModel> Roles { get; set; }

    public DbSet<ProductDbModel> Products { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
