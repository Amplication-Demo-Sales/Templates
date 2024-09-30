using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

public interface IContactsService
{
    /// <summary>
    /// Create one Contact
    /// </summary>
    public Task<Contact> CreateContact(ContactCreateInput contact);

    /// <summary>
    /// Delete one Contact
    /// </summary>
    public Task DeleteContact(ContactWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Contacts
    /// </summary>
    public Task<List<Contact>> Contacts(ContactFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Contact records
    /// </summary>
    public Task<MetadataDto> ContactsMeta(ContactFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Contact
    /// </summary>
    public Task<Contact> Contact(ContactWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Contact
    /// </summary>
    public Task UpdateContact(ContactWhereUniqueInput uniqueId, ContactUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Activities records to Contact
    /// </summary>
    public Task ConnectActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Disconnect multiple Activities records from Contact
    /// </summary>
    public Task DisconnectActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Find multiple Activities records for Contact
    /// </summary>
    public Task<List<Activity>> FindActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityFindManyArgs ActivityFindManyArgs
    );

    /// <summary>
    /// Update multiple Activities records for Contact
    /// </summary>
    public Task UpdateActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Get a Customer record for Contact
    /// </summary>
    public Task<Customer> GetCustomer(ContactWhereUniqueInput uniqueId);
}
