using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

public interface IActivitiesService
{
    /// <summary>
    /// Create one Activity
    /// </summary>
    public Task<Activity> CreateActivity(ActivityCreateInput activity);

    /// <summary>
    /// Delete one Activity
    /// </summary>
    public Task DeleteActivity(ActivityWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Activities
    /// </summary>
    public Task<List<Activity>> Activities(ActivityFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Activity records
    /// </summary>
    public Task<MetadataDto> ActivitiesMeta(ActivityFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Activity
    /// </summary>
    public Task<Activity> Activity(ActivityWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Activity
    /// </summary>
    public Task UpdateActivity(ActivityWhereUniqueInput uniqueId, ActivityUpdateInput updateDto);

    /// <summary>
    /// Get a Contact record for Activity
    /// </summary>
    public Task<Contact> GetContact(ActivityWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Customer record for Activity
    /// </summary>
    public Task<Customer> GetCustomer(ActivityWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Lead record for Activity
    /// </summary>
    public Task<Lead> GetLead(ActivityWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Opportunity record for Activity
    /// </summary>
    public Task<Opportunity> GetOpportunity(ActivityWhereUniqueInput uniqueId);
}
