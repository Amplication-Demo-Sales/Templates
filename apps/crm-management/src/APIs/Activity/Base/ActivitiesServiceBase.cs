using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class ActivitiesServiceBase : IActivitiesService
{
    protected readonly CrmManagementDbContext _context;

    public ActivitiesServiceBase(CrmManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Activity
    /// </summary>
    public async Task<Activity> CreateActivity(ActivityCreateInput createDto)
    {
        var activity = new ActivityDbModel
        {
            ActivityDate = createDto.ActivityDate,
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            RelatedTo = createDto.RelatedTo,
            Subject = createDto.Subject,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            activity.Id = createDto.Id;
        }
        if (createDto.Contact != null)
        {
            activity.Contact = await _context
                .Contacts.Where(contact => createDto.Contact.Id == contact.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Customer != null)
        {
            activity.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Lead != null)
        {
            activity.Lead = await _context
                .Leads.Where(lead => createDto.Lead.Id == lead.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Opportunity != null)
        {
            activity.Opportunity = await _context
                .Opportunities.Where(opportunity => createDto.Opportunity.Id == opportunity.Id)
                .FirstOrDefaultAsync();
        }

        _context.Activities.Add(activity);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ActivityDbModel>(activity.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Activity
    /// </summary>
    public async Task DeleteActivity(ActivityWhereUniqueInput uniqueId)
    {
        var activity = await _context.Activities.FindAsync(uniqueId.Id);
        if (activity == null)
        {
            throw new NotFoundException();
        }

        _context.Activities.Remove(activity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Activities
    /// </summary>
    public async Task<List<Activity>> Activities(ActivityFindManyArgs findManyArgs)
    {
        var activities = await _context
            .Activities.Include(x => x.Customer)
            .Include(x => x.Lead)
            .Include(x => x.Opportunity)
            .Include(x => x.Contact)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return activities.ConvertAll(activity => activity.ToDto());
    }

    /// <summary>
    /// Meta data about Activity records
    /// </summary>
    public async Task<MetadataDto> ActivitiesMeta(ActivityFindManyArgs findManyArgs)
    {
        var count = await _context.Activities.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Activity
    /// </summary>
    public async Task<Activity> Activity(ActivityWhereUniqueInput uniqueId)
    {
        var activities = await this.Activities(
            new ActivityFindManyArgs { Where = new ActivityWhereInput { Id = uniqueId.Id } }
        );
        var activity = activities.FirstOrDefault();
        if (activity == null)
        {
            throw new NotFoundException();
        }

        return activity;
    }

    /// <summary>
    /// Update one Activity
    /// </summary>
    public async Task UpdateActivity(
        ActivityWhereUniqueInput uniqueId,
        ActivityUpdateInput updateDto
    )
    {
        var activity = updateDto.ToModel(uniqueId);

        if (updateDto.Contact != null)
        {
            activity.Contact = await _context
                .Contacts.Where(contact => updateDto.Contact == contact.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Customer != null)
        {
            activity.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Lead != null)
        {
            activity.Lead = await _context
                .Leads.Where(lead => updateDto.Lead == lead.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Opportunity != null)
        {
            activity.Opportunity = await _context
                .Opportunities.Where(opportunity => updateDto.Opportunity == opportunity.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(activity).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Activities.Any(e => e.Id == activity.Id))
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
    /// Get a Contact record for Activity
    /// </summary>
    public async Task<Contact> GetContact(ActivityWhereUniqueInput uniqueId)
    {
        var activity = await _context
            .Activities.Where(activity => activity.Id == uniqueId.Id)
            .Include(activity => activity.Contact)
            .FirstOrDefaultAsync();
        if (activity == null)
        {
            throw new NotFoundException();
        }
        return activity.Contact.ToDto();
    }

    /// <summary>
    /// Get a Customer record for Activity
    /// </summary>
    public async Task<Customer> GetCustomer(ActivityWhereUniqueInput uniqueId)
    {
        var activity = await _context
            .Activities.Where(activity => activity.Id == uniqueId.Id)
            .Include(activity => activity.Customer)
            .FirstOrDefaultAsync();
        if (activity == null)
        {
            throw new NotFoundException();
        }
        return activity.Customer.ToDto();
    }

    /// <summary>
    /// Get a Lead record for Activity
    /// </summary>
    public async Task<Lead> GetLead(ActivityWhereUniqueInput uniqueId)
    {
        var activity = await _context
            .Activities.Where(activity => activity.Id == uniqueId.Id)
            .Include(activity => activity.Lead)
            .FirstOrDefaultAsync();
        if (activity == null)
        {
            throw new NotFoundException();
        }
        return activity.Lead.ToDto();
    }

    /// <summary>
    /// Get a Opportunity record for Activity
    /// </summary>
    public async Task<Opportunity> GetOpportunity(ActivityWhereUniqueInput uniqueId)
    {
        var activity = await _context
            .Activities.Where(activity => activity.Id == uniqueId.Id)
            .Include(activity => activity.Opportunity)
            .FirstOrDefaultAsync();
        if (activity == null)
        {
            throw new NotFoundException();
        }
        return activity.Opportunity.ToDto();
    }
}
