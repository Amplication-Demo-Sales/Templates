using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;

namespace ReservationManagement.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<User> CreateUser(UserCreateInput user);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<User>> Users(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<User> User(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Reservations records to User
    /// </summary>
    public Task ConnectReservations(
        UserWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Disconnect multiple Reservations records from User
    /// </summary>
    public Task DisconnectReservations(
        UserWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Find multiple Reservations records for User
    /// </summary>
    public Task<List<Reservation>> FindReservations(
        UserWhereUniqueInput uniqueId,
        ReservationFindManyArgs ReservationFindManyArgs
    );

    /// <summary>
    /// Update multiple Reservations records for User
    /// </summary>
    public Task UpdateReservations(
        UserWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Connect multiple Reviews records to User
    /// </summary>
    public Task ConnectReviews(UserWhereUniqueInput uniqueId, ReviewWhereUniqueInput[] reviewsId);

    /// <summary>
    /// Disconnect multiple Reviews records from User
    /// </summary>
    public Task DisconnectReviews(
        UserWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] reviewsId
    );

    /// <summary>
    /// Find multiple Reviews records for User
    /// </summary>
    public Task<List<Review>> FindReviews(
        UserWhereUniqueInput uniqueId,
        ReviewFindManyArgs ReviewFindManyArgs
    );

    /// <summary>
    /// Update multiple Reviews records for User
    /// </summary>
    public Task UpdateReviews(UserWhereUniqueInput uniqueId, ReviewWhereUniqueInput[] reviewsId);
}
