using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class OpportunitiesServiceBase : IOpportunitiesService
{
    protected readonly CrmManagementDbContext _context;

    public OpportunitiesServiceBase(CrmManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Opportunity
    /// </summary>
    public async Task<Opportunity> CreateOpportunity(OpportunityCreateInput createDto)
    {
        var opportunity = new OpportunityDbModel
        {
            Amount = createDto.Amount,
            CloseDate = createDto.CloseDate,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Stage = createDto.Stage,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            opportunity.Id = createDto.Id;
        }
        if (createDto.Activities != null)
        {
            opportunity.Activities = await _context
                .Activities.Where(activity =>
                    createDto.Activities.Select(t => t.Id).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (createDto.Customer != null)
        {
            opportunity.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Opportunities.Add(opportunity);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OpportunityDbModel>(opportunity.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Opportunity
    /// </summary>
    public async Task DeleteOpportunity(OpportunityWhereUniqueInput uniqueId)
    {
        var opportunity = await _context.Opportunities.FindAsync(uniqueId.Id);
        if (opportunity == null)
        {
            throw new NotFoundException();
        }

        _context.Opportunities.Remove(opportunity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Opportunities
    /// </summary>
    public async Task<List<Opportunity>> Opportunities(OpportunityFindManyArgs findManyArgs)
    {
        var opportunities = await _context
            .Opportunities.Include(x => x.Customer)
            .Include(x => x.Activities)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return opportunities.ConvertAll(opportunity => opportunity.ToDto());
    }

    /// <summary>
    /// Meta data about Opportunity records
    /// </summary>
    public async Task<MetadataDto> OpportunitiesMeta(OpportunityFindManyArgs findManyArgs)
    {
        var count = await _context.Opportunities.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Opportunity
    /// </summary>
    public async Task<Opportunity> Opportunity(OpportunityWhereUniqueInput uniqueId)
    {
        var opportunities = await this.Opportunities(
            new OpportunityFindManyArgs { Where = new OpportunityWhereInput { Id = uniqueId.Id } }
        );
        var opportunity = opportunities.FirstOrDefault();
        if (opportunity == null)
        {
            throw new NotFoundException();
        }

        return opportunity;
    }

    /// <summary>
    /// Update one Opportunity
    /// </summary>
    public async Task UpdateOpportunity(
        OpportunityWhereUniqueInput uniqueId,
        OpportunityUpdateInput updateDto
    )
    {
        var opportunity = updateDto.ToModel(uniqueId);

        if (updateDto.Activities != null)
        {
            opportunity.Activities = await _context
                .Activities.Where(activity =>
                    updateDto.Activities.Select(t => t).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Customer != null)
        {
            opportunity.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(opportunity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Opportunities.Any(e => e.Id == opportunity.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Activities records to Opportunity
    /// </summary>
    public async Task ConnectActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Opportunities.Include(x => x.Activities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Activities.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Activities);

        foreach (var child in childrenToConnect)
        {
            parent.Activities.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Activities records from Opportunity
    /// </summary>
    public async Task DisconnectActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Opportunities.Include(x => x.Activities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Activities.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Activities?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Activities records for Opportunity
    /// </summary>
    public async Task<List<Activity>> FindActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityFindManyArgs opportunityFindManyArgs
    )
    {
        var activities = await _context
            .Activities.Where(m => m.OpportunityId == uniqueId.Id)
            .ApplyWhere(opportunityFindManyArgs.Where)
            .ApplySkip(opportunityFindManyArgs.Skip)
            .ApplyTake(opportunityFindManyArgs.Take)
            .ApplyOrderBy(opportunityFindManyArgs.SortBy)
            .ToListAsync();

        return activities.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Activities records for Opportunity
    /// </summary>
    public async Task UpdateActivities(
        OpportunityWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var opportunity = await _context
            .Opportunities.Include(t => t.Activities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (opportunity == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Activities.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        opportunity.Activities = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Customer record for Opportunity
    /// </summary>
    public async Task<Customer> GetCustomer(OpportunityWhereUniqueInput uniqueId)
    {
        var opportunity = await _context
            .Opportunities.Where(opportunity => opportunity.Id == uniqueId.Id)
            .Include(opportunity => opportunity.Customer)
            .FirstOrDefaultAsync();
        if (opportunity == null)
        {
            throw new NotFoundException();
        }
        return opportunity.Customer.ToDto();
    }
}
