using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.Infrastructure;

public class CrmManagementDbContext : DbContext
{
    public CrmManagementDbContext(DbContextOptions<CrmManagementDbContext> options)
        : base(options) { }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<LeadDbModel> Leads { get; set; }

    public DbSet<ActivityDbModel> Activities { get; set; }

    public DbSet<OpportunityDbModel> Opportunities { get; set; }

    public DbSet<ContactDbModel> Contacts { get; set; }

    public DbSet<UserDbModel> Users { get; set; }

    public DbSet<RoomDbModel> Rooms { get; set; }

    public DbSet<ReservationDbModel> Reservations { get; set; }

    public DbSet<ServiceDbModel> Services { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }
}
