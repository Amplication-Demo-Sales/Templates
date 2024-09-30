using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Activities records to Customer
    /// </summary>
    public Task ConnectActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Disconnect multiple Activities records from Customer
    /// </summary>
    public Task DisconnectActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Find multiple Activities records for Customer
    /// </summary>
    public Task<List<Activity>> FindActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityFindManyArgs ActivityFindManyArgs
    );

    /// <summary>
    /// Update multiple Activities records for Customer
    /// </summary>
    public Task UpdateActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Connect multiple Contacts records to Customer
    /// </summary>
    public Task ConnectContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactWhereUniqueInput[] contactsId
    );

    /// <summary>
    /// Disconnect multiple Contacts records from Customer
    /// </summary>
    public Task DisconnectContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactWhereUniqueInput[] contactsId
    );

    /// <summary>
    /// Find multiple Contacts records for Customer
    /// </summary>
    public Task<List<Contact>> FindContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactFindManyArgs ContactFindManyArgs
    );

    /// <summary>
    /// Update multiple Contacts records for Customer
    /// </summary>
    public Task UpdateContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactWhereUniqueInput[] contactsId
    );

    /// <summary>
    /// Connect multiple Leads records to Customer
    /// </summary>
    public Task ConnectLeads(CustomerWhereUniqueInput uniqueId, LeadWhereUniqueInput[] leadsId);

    /// <summary>
    /// Disconnect multiple Leads records from Customer
    /// </summary>
    public Task DisconnectLeads(CustomerWhereUniqueInput uniqueId, LeadWhereUniqueInput[] leadsId);

    /// <summary>
    /// Find multiple Leads records for Customer
    /// </summary>
    public Task<List<Lead>> FindLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadFindManyArgs LeadFindManyArgs
    );

    /// <summary>
    /// Update multiple Leads records for Customer
    /// </summary>
    public Task UpdateLeads(CustomerWhereUniqueInput uniqueId, LeadWhereUniqueInput[] leadsId);

    /// <summary>
    /// Connect multiple Opportunities records to Customer
    /// </summary>
    public Task ConnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] opportunitiesId
    );

    /// <summary>
    /// Disconnect multiple Opportunities records from Customer
    /// </summary>
    public Task DisconnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] opportunitiesId
    );

    /// <summary>
    /// Find multiple Opportunities records for Customer
    /// </summary>
    public Task<List<Opportunity>> FindOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityFindManyArgs OpportunityFindManyArgs
    );

    /// <summary>
    /// Update multiple Opportunities records for Customer
    /// </summary>
    public Task UpdateOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] opportunitiesId
    );

    /// <summary>
    /// Connect multiple Reservations records to Customer
    /// </summary>
    public Task ConnectReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Disconnect multiple Reservations records from Customer
    /// </summary>
    public Task DisconnectReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );

    /// <summary>
    /// Find multiple Reservations records for Customer
    /// </summary>
    public Task<List<Reservation>> FindReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationFindManyArgs ReservationFindManyArgs
    );

    /// <summary>
    /// Update multiple Reservations records for Customer
    /// </summary>
    public Task UpdateReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] reservationsId
    );
}
