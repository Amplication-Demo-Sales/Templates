using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;

namespace ReservationManagement.APIs;

public interface IReservationsService
{
    /// <summary>
    /// Create one Reservation
    /// </summary>
    public Task<Reservation> CreateReservation(ReservationCreateInput reservation);

    /// <summary>
    /// Delete one Reservation
    /// </summary>
    public Task DeleteReservation(ReservationWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Reservations
    /// </summary>
    public Task<List<Reservation>> Reservations(ReservationFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Reservation records
    /// </summary>
    public Task<MetadataDto> ReservationsMeta(ReservationFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Reservation
    /// </summary>
    public Task<Reservation> Reservation(ReservationWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Reservation
    /// </summary>
    public Task UpdateReservation(
        ReservationWhereUniqueInput uniqueId,
        ReservationUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Payments records to Reservation
    /// </summary>
    public Task ConnectPayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] paymentsId
    );

    /// <summary>
    /// Disconnect multiple Payments records from Reservation
    /// </summary>
    public Task DisconnectPayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] paymentsId
    );

    /// <summary>
    /// Find multiple Payments records for Reservation
    /// </summary>
    public Task<List<Payment>> FindPayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentFindManyArgs PaymentFindManyArgs
    );

    /// <summary>
    /// Update multiple Payments records for Reservation
    /// </summary>
    public Task UpdatePayments(
        ReservationWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] paymentsId
    );

    /// <summary>
    /// Connect multiple Reviews records to Reservation
    /// </summary>
    public Task ConnectReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] reviewsId
    );

    /// <summary>
    /// Disconnect multiple Reviews records from Reservation
    /// </summary>
    public Task DisconnectReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] reviewsId
    );

    /// <summary>
    /// Find multiple Reviews records for Reservation
    /// </summary>
    public Task<List<Review>> FindReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewFindManyArgs ReviewFindManyArgs
    );

    /// <summary>
    /// Update multiple Reviews records for Reservation
    /// </summary>
    public Task UpdateReviews(
        ReservationWhereUniqueInput uniqueId,
        ReviewWhereUniqueInput[] reviewsId
    );

    /// <summary>
    /// Get a Room record for Reservation
    /// </summary>
    public Task<Room> GetRoom(ReservationWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a User record for Reservation
    /// </summary>
    public Task<User> GetUser(ReservationWhereUniqueInput uniqueId);
}
