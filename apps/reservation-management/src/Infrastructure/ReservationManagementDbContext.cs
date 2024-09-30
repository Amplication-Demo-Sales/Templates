using Microsoft.EntityFrameworkCore;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.Infrastructure;

public class ReservationManagementDbContext : DbContext
{
    public ReservationManagementDbContext(DbContextOptions<ReservationManagementDbContext> options)
        : base(options) { }

    public DbSet<PaymentDbModel> Payments { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<ReviewDbModel> Reviews { get; set; }

    public DbSet<ReservationDbModel> Reservations { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
