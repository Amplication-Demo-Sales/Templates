using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly CrmManagementDbContext _context;

    public CustomersServiceBase(CrmManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<Customer> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new CustomerDbModel
        {
            Address = createDto.Address,
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Name = createDto.Name,
            Phone = createDto.Phone,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Activities != null)
        {
            customer.Activities = await _context
                .Activities.Where(activity =>
                    createDto.Activities.Select(t => t.Id).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (createDto.Contacts != null)
        {
            customer.Contacts = await _context
                .Contacts.Where(contact =>
                    createDto.Contacts.Select(t => t.Id).Contains(contact.Id)
                )
                .ToListAsync();
        }

        if (createDto.Leads != null)
        {
            customer.Leads = await _context
                .Leads.Where(lead => createDto.Leads.Select(t => t.Id).Contains(lead.Id))
                .ToListAsync();
        }

        if (createDto.Opportunities != null)
        {
            customer.Opportunities = await _context
                .Opportunities.Where(opportunity =>
                    createDto.Opportunities.Select(t => t.Id).Contains(opportunity.Id)
                )
                .ToListAsync();
        }

        if (createDto.Reservations != null)
        {
            customer.Reservations = await _context
                .Reservations.Where(reservation =>
                    createDto.Reservations.Select(t => t.Id).Contains(reservation.Id)
                )
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CustomerDbModel>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerWhereUniqueInput uniqueId)
    {
        var customer = await _context.Customers.FindAsync(uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Leads)
            .Include(x => x.Activities)
            .Include(x => x.Opportunities)
            .Include(x => x.Contacts)
            .Include(x => x.Reservations)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<Customer> Customer(CustomerWhereUniqueInput uniqueId)
    {
        var customers = await this.Customers(
            new CustomerFindManyArgs { Where = new CustomerWhereInput { Id = uniqueId.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(
        CustomerWhereUniqueInput uniqueId,
        CustomerUpdateInput updateDto
    )
    {
        var customer = updateDto.ToModel(uniqueId);

        if (updateDto.Activities != null)
        {
            customer.Activities = await _context
                .Activities.Where(activity =>
                    updateDto.Activities.Select(t => t).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Contacts != null)
        {
            customer.Contacts = await _context
                .Contacts.Where(contact => updateDto.Contacts.Select(t => t).Contains(contact.Id))
                .ToListAsync();
        }

        if (updateDto.Leads != null)
        {
            customer.Leads = await _context
                .Leads.Where(lead => updateDto.Leads.Select(t => t).Contains(lead.Id))
                .ToListAsync();
        }

        if (updateDto.Opportunities != null)
        {
            customer.Opportunities = await _context
                .Opportunities.Where(opportunity =>
                    updateDto.Opportunities.Select(t => t).Contains(opportunity.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Reservations != null)
        {
            customer.Reservations = await _context
                .Reservations.Where(reservation =>
                    updateDto.Reservations.Select(t => t).Contains(reservation.Id)
                )
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
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
    /// Connect multiple Activities records to Customer
    /// </summary>
    public async Task ConnectActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Activities)
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
    /// Disconnect multiple Activities records from Customer
    /// </summary>
    public async Task DisconnectActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Activities)
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
    /// Find multiple Activities records for Customer
    /// </summary>
    public async Task<List<Activity>> FindActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityFindManyArgs customerFindManyArgs
    )
    {
        var activities = await _context
            .Activities.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return activities.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Activities records for Customer
    /// </summary>
    public async Task UpdateActivities(
        CustomerWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Activities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
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

        customer.Activities = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Contacts records to Customer
    /// </summary>
    public async Task ConnectContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Contacts.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Contacts);

        foreach (var child in childrenToConnect)
        {
            parent.Contacts.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Contacts records from Customer
    /// </summary>
    public async Task DisconnectContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Contacts)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Contacts.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Contacts?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Contacts records for Customer
    /// </summary>
    public async Task<List<Contact>> FindContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactFindManyArgs customerFindManyArgs
    )
    {
        var contacts = await _context
            .Contacts.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return contacts.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Contacts records for Customer
    /// </summary>
    public async Task UpdateContacts(
        CustomerWhereUniqueInput uniqueId,
        ContactWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Contacts)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Contacts.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Contacts = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Leads records to Customer
    /// </summary>
    public async Task ConnectLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Leads)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Leads.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Leads);

        foreach (var child in childrenToConnect)
        {
            parent.Leads.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Leads records from Customer
    /// </summary>
    public async Task DisconnectLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Leads)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Leads.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Leads?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Leads records for Customer
    /// </summary>
    public async Task<List<Lead>> FindLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadFindManyArgs customerFindManyArgs
    )
    {
        var leads = await _context
            .Leads.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return leads.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Leads records for Customer
    /// </summary>
    public async Task UpdateLeads(
        CustomerWhereUniqueInput uniqueId,
        LeadWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Leads)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Leads.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Leads = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Opportunities records to Customer
    /// </summary>
    public async Task ConnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Opportunities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Opportunities.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Opportunities);

        foreach (var child in childrenToConnect)
        {
            parent.Opportunities.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Opportunities records from Customer
    /// </summary>
    public async Task DisconnectOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Opportunities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Opportunities.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Opportunities?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Opportunities records for Customer
    /// </summary>
    public async Task<List<Opportunity>> FindOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityFindManyArgs customerFindManyArgs
    )
    {
        var opportunities = await _context
            .Opportunities.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return opportunities.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Opportunities records for Customer
    /// </summary>
    public async Task UpdateOpportunities(
        CustomerWhereUniqueInput uniqueId,
        OpportunityWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Opportunities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Opportunities.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Opportunities = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Reservations records to Customer
    /// </summary>
    public async Task ConnectReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Reservations);

        foreach (var child in childrenToConnect)
        {
            parent.Reservations.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Reservations records from Customer
    /// </summary>
    public async Task DisconnectReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Customers.Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Reservations?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Reservations records for Customer
    /// </summary>
    public async Task<List<Reservation>> FindReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationFindManyArgs customerFindManyArgs
    )
    {
        var reservations = await _context
            .Reservations.Where(m => m.CustomerId == uniqueId.Id)
            .ApplyWhere(customerFindManyArgs.Where)
            .ApplySkip(customerFindManyArgs.Skip)
            .ApplyTake(customerFindManyArgs.Take)
            .ApplyOrderBy(customerFindManyArgs.SortBy)
            .ToListAsync();

        return reservations.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Reservations records for Customer
    /// </summary>
    public async Task UpdateReservations(
        CustomerWhereUniqueInput uniqueId,
        ReservationWhereUniqueInput[] childrenIds
    )
    {
        var customer = await _context
            .Customers.Include(t => t.Reservations)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Reservations.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Reservations = children;
        await _context.SaveChangesAsync();
    }
}
