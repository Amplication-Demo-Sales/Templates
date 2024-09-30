using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;

namespace CrmManagement.APIs;

public interface IOpportunitiesService
{
    /// <summary>
    /// Create one Opportunity
    /// </summary>
    public Task<Opportunity> CreateOpportunity(OpportunityCreateInput opportunity);

    /// <summary>
    /// Delete one Opportunity
    /// </summary>
    public Task DeleteOpportunity(OpportunityWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Opportunities
    /// </summary>
    public Task<List<Opportunity>> Opportunities(OpportunityFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Opportunity records
    /// </summary>
    public Task<MetadataDto> OpportunitiesMeta(OpportunityFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Opportunity
    /// </summary>
    public Task<Opportunity> Opportunity(OpportunityWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Opportunity
    /// </summary>
    public Task UpdateOpportunity(
        OpportunityWhereUniqueInput uniqueId,
        OpportunityUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Activities records to Opportunity
    /// </summary>
    public Task ConnectActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Disconnect multiple Activities records from Opportunity
    /// </summary>
    public Task DisconnectActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Find multiple Activities records for Opportunity
    /// </summary>
    public Task<List<Activity>> FindActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityFindManyArgs ActivityFindManyArgs
    );

    /// <summary>
    /// Update multiple Activities records for Opportunity
    /// </summary>
    public Task UpdateActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] activitiesId
    );

    /// <summary>
    /// Get a Customer record for Opportunity
    /// </summary>
    public Task<Customer> GetCustomer(OpportunityWhereUniqueInput uniqueId);
}
