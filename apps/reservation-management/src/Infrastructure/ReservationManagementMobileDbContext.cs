using Microsoft.EntityFrameworkCore;
using ReservationManagementMobile.Infrastructure.Models;

namespace ReservationManagementMobile.Infrastructure;

public class ReservationManagementMobileDbContext : DbContext
{
    public ReservationManagementMobileDbContext(
        DbContextOptions<ReservationManagementMobileDbContext> options
    )
        : base(options) { }

    public DbSet<ReservationDbModel> Reservations { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }
}
