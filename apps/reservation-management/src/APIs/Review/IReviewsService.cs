using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;

namespace ReservationManagement.APIs;

public interface IReviewsService
{
    /// <summary>
    /// Create one Review
    /// </summary>
    public Task<Review> CreateReview(ReviewCreateInput review);

    /// <summary>
    /// Delete one Review
    /// </summary>
    public Task DeleteReview(ReviewWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Reviews
    /// </summary>
    public Task<List<Review>> Reviews(ReviewFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Review records
    /// </summary>
    public Task<MetadataDto> ReviewsMeta(ReviewFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Review
    /// </summary>
    public Task<Review> Review(ReviewWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Review
    /// </summary>
    public Task UpdateReview(ReviewWhereUniqueInput uniqueId, ReviewUpdateInput updateDto);

    /// <summary>
    /// Get a Reservation record for Review
    /// </summary>
    public Task<Reservation> GetReservation(ReviewWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a User record for Review
    /// </summary>
    public Task<User> GetUser(ReviewWhereUniqueInput uniqueId);
}
