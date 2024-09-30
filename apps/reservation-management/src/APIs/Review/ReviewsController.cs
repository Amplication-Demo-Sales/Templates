using Microsoft.AspNetCore.Mvc;

namespace ReservationManagement.APIs;

[ApiController()]
public class ReviewsController : ReviewsControllerBase
{
    public ReviewsController(IReviewsService service)
        : base(service) { }
}
