using CarRentalManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Infrastructure;

public class CarRentalManagementDbContext : DbContext
{
    public CarRentalManagementDbContext(DbContextOptions<CarRentalManagementDbContext> options)
        : base(options) { }

    public DbSet<ProductDbModel> Products { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
