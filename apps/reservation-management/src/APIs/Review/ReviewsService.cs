using ReservationManagement.Infrastructure;

namespace ReservationManagement.APIs;

public class ReviewsService : ReviewsServiceBase
{
    public ReviewsService(ReservationManagementDbContext context)
        : base(context) { }
}
