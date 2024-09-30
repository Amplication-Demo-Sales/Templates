using ReservationManagement.APIs;

namespace ReservationManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IReservationsService, ReservationsService>();
        services.AddScoped<IReviewsService, ReviewsService>();
        services.AddScoped<IRoomsService, RoomsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
