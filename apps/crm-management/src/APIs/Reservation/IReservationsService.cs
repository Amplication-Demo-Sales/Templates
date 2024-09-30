using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

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
    /// Get a Customer record for Reservation
    /// </summary>
    public Task<Customer> GetCustomer(ReservationWhereUniqueInput uniqueId);

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
    /// Get a Room record for Reservation
    /// </summary>
    public Task<Room> GetRoom(ReservationWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Services records to Reservation
    /// </summary>
    public Task ConnectServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceWhereUniqueInput[] servicesId
    );

    /// <summary>
    /// Disconnect multiple Services records from Reservation
    /// </summary>
    public Task DisconnectServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceWhereUniqueInput[] servicesId
    );

    /// <summary>
    /// Find multiple Services records for Reservation
    /// </summary>
    public Task<List<Service>> FindServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceFindManyArgs ServiceFindManyArgs
    );

    /// <summary>
    /// Update multiple Services records for Reservation
    /// </summary>
    public Task UpdateServices(
        ReservationWhereUniqueInput uniqueId,
        ServiceWhereUniqueInput[] servicesId
    );
}
