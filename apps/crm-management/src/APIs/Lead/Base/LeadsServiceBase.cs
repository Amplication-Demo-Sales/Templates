using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class LeadsServiceBase : ILeadsService
{
    protected readonly CrmManagementDbContext _context;

    public LeadsServiceBase(CrmManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Lead
    /// </summary>
    public async Task<Lead> CreateLead(LeadCreateInput createDto)
    {
        var lead = new LeadDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            Source = createDto.Source,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            lead.Id = createDto.Id;
        }
        if (createDto.Activities != null)
        {
            lead.Activities = await _context
                .Activities.Where(activity =>
                    createDto.Activities.Select(t => t.Id).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (createDto.Customer != null)
        {
            lead.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<LeadDbModel>(lead.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Lead
    /// </summary>
    public async Task DeleteLead(LeadWhereUniqueInput uniqueId)
    {
        var lead = await _context.Leads.FindAsync(uniqueId.Id);
        if (lead == null)
        {
            throw new NotFoundException();
        }

        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Leads
    /// </summary>
    public async Task<List<Lead>> Leads(LeadFindManyArgs findManyArgs)
    {
        var leads = await _context
            .Leads.Include(x => x.Customer)
            .Include(x => x.Activities)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return leads.ConvertAll(lead => lead.ToDto());
    }

    /// <summary>
    /// Meta data about Lead records
    /// </summary>
    public async Task<MetadataDto> LeadsMeta(LeadFindManyArgs findManyArgs)
    {
        var count = await _context.Leads.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Lead
    /// </summary>
    public async Task<Lead> Lead(LeadWhereUniqueInput uniqueId)
    {
        var leads = await this.Leads(
            new LeadFindManyArgs { Where = new LeadWhereInput { Id = uniqueId.Id } }
        );
        var lead = leads.FirstOrDefault();
        if (lead == null)
        {
            throw new NotFoundException();
        }

        return lead;
    }

    /// <summary>
    /// Update one Lead
    /// </summary>
    public async Task UpdateLead(LeadWhereUniqueInput uniqueId, LeadUpdateInput updateDto)
    {
        var lead = updateDto.ToModel(uniqueId);

        if (updateDto.Activities != null)
        {
            lead.Activities = await _context
                .Activities.Where(activity =>
                    updateDto.Activities.Select(t => t).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Customer != null)
        {
            lead.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(lead).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Leads.Any(e => e.Id == lead.Id))
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
    /// Connect multiple Activities records to Lead
    /// </summary>
    public async Task ConnectActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Leads.Include(x => x.Activities)
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
    /// Disconnect multiple Activities records from Lead
    /// </summary>
    public async Task DisconnectActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Leads.Include(x => x.Activities)
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
    /// Find multiple Activities records for Lead
    /// </summary>
    public async Task<List<Activity>> FindActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityFindManyArgs leadFindManyArgs
    )
    {
        var activities = await _context
            .Activities.Where(m => m.LeadId == uniqueId.Id)
            .ApplyWhere(leadFindManyArgs.Where)
            .ApplySkip(leadFindManyArgs.Skip)
            .ApplyTake(leadFindManyArgs.Take)
            .ApplyOrderBy(leadFindManyArgs.SortBy)
            .ToListAsync();

        return activities.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Activities records for Lead
    /// </summary>
    public async Task UpdateActivities(
        LeadWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var lead = await _context
            .Leads.Include(t => t.Activities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (lead == null)
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

        lead.Activities = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Customer record for Lead
    /// </summary>
    public async Task<Customer> GetCustomer(LeadWhereUniqueInput uniqueId)
    {
        var lead = await _context
            .Leads.Where(lead => lead.Id == uniqueId.Id)
            .Include(lead => lead.Customer)
            .FirstOrDefaultAsync();
        if (lead == null)
        {
            throw new NotFoundException();
        }
        return lead.Customer.ToDto();
    }
}
