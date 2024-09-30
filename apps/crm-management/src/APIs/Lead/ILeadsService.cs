using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

public interface ILeadsService
{
    /// <summary>
    /// Create one Lead
    /// </summary>
    public Task<Lead> CreateLead(LeadCreateInput lead);

    /// <summary>
    /// Delete one Lead
    /// </summary>
    public Task DeleteLead(LeadWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Leads
    /// </summary>
    public Task<List<Lead>> Leads(LeadFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Lead records
    /// </summary>
    public Task<MetadataDto> LeadsMeta(LeadFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Lead
    /// </summary>
    public Task<Lead> Lead(LeadWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Lead
    /// </summary>
    public Task UpdateLead(LeadWhereUniqueInput uniqueId, LeadUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Activities records to Lead
    /// </summary>
    public Task ConnectActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Disconnect multiple Activities records from Lead
    /// </summary>
    public Task DisconnectActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Find multiple Activities records for Lead
    /// </summary>
    public Task<List<Activity>> FindActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityFindManyArgs ActivityFindManyArgs
    );

    /// <summary>
    /// Update multiple Activities records for Lead
    /// </summary>
    public Task UpdateActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Get a Customer record for Lead
    /// </summary>
    public Task<Customer> GetCustomer(LeadWhereUniqueInput uniqueId);
}
