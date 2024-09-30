using CrmManagement.APIs;

namespace CrmManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IActivitiesService, ActivitiesService>();
        services.AddScoped<IContactsService, ContactsService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<ILeadsService, LeadsService>();
        services.AddScoped<IOpportunitiesService, OpportunitiesService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IReservationsService, ReservationsService>();
        services.AddScoped<IRoomsService, RoomsService>();
        services.AddScoped<IServicesService, ServicesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
