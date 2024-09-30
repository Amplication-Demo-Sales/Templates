using CrmManagement.APIs;
using CrmManagement.APIs.Common;
using CrmManagement.APIs.Dtos;
using CrmManagement.APIs.Errors;
using CrmManagement.APIs.Extensions;
using CrmManagement.Infrastructure;
using CrmManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmManagement.APIs;

public abstract class ContactsServiceBase : IContactsService
{
    protected readonly CrmManagementDbContext _context;

    public ContactsServiceBase(CrmManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Contact
    /// </summary>
    public async Task<Contact> CreateContact(ContactCreateInput createDto)
    {
        var contact = new ContactDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Phone = createDto.Phone,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            contact.Id = createDto.Id;
        }
        if (createDto.Activities != null)
        {
            contact.Activities = await _context
                .Activities.Where(activity =>
                    createDto.Activities.Select(t => t.Id).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (createDto.Customer != null)
        {
            contact.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ContactDbModel>(contact.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Contact
    /// </summary>
    public async Task DeleteContact(ContactWhereUniqueInput uniqueId)
    {
        var contact = await _context.Contacts.FindAsync(uniqueId.Id);
        if (contact == null)
        {
            throw new NotFoundException();
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Contacts
    /// </summary>
    public async Task<List<Contact>> Contacts(ContactFindManyArgs findManyArgs)
    {
        var contacts = await _context
            .Contacts.Include(x => x.Customer)
            .Include(x => x.Activities)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return contacts.ConvertAll(contact => contact.ToDto());
    }

    /// <summary>
    /// Meta data about Contact records
    /// </summary>
    public async Task<MetadataDto> ContactsMeta(ContactFindManyArgs findManyArgs)
    {
        var count = await _context.Contacts.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Contact
    /// </summary>
    public async Task<Contact> Contact(ContactWhereUniqueInput uniqueId)
    {
        var contacts = await this.Contacts(
            new ContactFindManyArgs { Where = new ContactWhereInput { Id = uniqueId.Id } }
        );
        var contact = contacts.FirstOrDefault();
        if (contact == null)
        {
            throw new NotFoundException();
        }

        return contact;
    }

    /// <summary>
    /// Update one Contact
    /// </summary>
    public async Task UpdateContact(ContactWhereUniqueInput uniqueId, ContactUpdateInput updateDto)
    {
        var contact = updateDto.ToModel(uniqueId);

        if (updateDto.Activities != null)
        {
            contact.Activities = await _context
                .Activities.Where(activity =>
                    updateDto.Activities.Select(t => t).Contains(activity.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Customer != null)
        {
            contact.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(contact).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Contacts.Any(e => e.Id == contact.Id))
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
    /// Connect multiple Activities records to Contact
    /// </summary>
    public async Task ConnectActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Contacts.Include(x => x.Activities)
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
    /// Disconnect multiple Activities records from Contact
    /// </summary>
    public async Task DisconnectActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Contacts.Include(x => x.Activities)
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
    /// Find multiple Activities records for Contact
    /// </summary>
    public async Task<List<Activity>> FindActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityFindManyArgs contactFindManyArgs
    )
    {
        var activities = await _context
            .Activities.Where(m => m.ContactId == uniqueId.Id)
            .ApplyWhere(contactFindManyArgs.Where)
            .ApplySkip(contactFindManyArgs.Skip)
            .ApplyTake(contactFindManyArgs.Take)
            .ApplyOrderBy(contactFindManyArgs.SortBy)
            .ToListAsync();

        return activities.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Activities records for Contact
    /// </summary>
    public async Task UpdateActivities(
        ContactWhereUniqueInput uniqueId,
        ActivityWhereUniqueInput[] childrenIds
    )
    {
        var contact = await _context
            .Contacts.Include(t => t.Activities)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (contact == null)
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

        contact.Activities = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Customer record for Contact
    /// </summary>
    public async Task<Customer> GetCustomer(ContactWhereUniqueInput uniqueId)
    {
        var contact = await _context
            .Contacts.Where(contact => contact.Id == uniqueId.Id)
            .Include(contact => contact.Customer)
            .FirstOrDefaultAsync();
        if (contact == null)
        {
            throw new NotFoundException();
        }
        return contact.Customer.ToDto();
    }
}
