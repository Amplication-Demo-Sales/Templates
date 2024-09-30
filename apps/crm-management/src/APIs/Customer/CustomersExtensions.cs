using CrmManagement.APIs.Dtos;
using CrmManagement.Infrastructure.Models;

namespace CrmManagement.APIs.Extensions;

public static class CustomersExtensions
{
    public static Customer ToDto(this CustomerDbModel model)
    {
        return new Customer
        {
            Activities = model.Activities?.Select(x => x.Id).ToList(),
            Address = model.Address,
            Contacts = model.Contacts?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            Id = model.Id,
            Leads = model.Leads?.Select(x => x.Id).ToList(),
            Name = model.Name,
            Opportunities = model.Opportunities?.Select(x => x.Id).ToList(),
            Phone = model.Phone,
            Reservations = model.Reservations?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CustomerDbModel ToModel(
        this CustomerUpdateInput updateDto,
        CustomerWhereUniqueInput uniqueId
    )
    {
        var customer = new CustomerDbModel
        {
            Id = uniqueId.Id,
            Address = updateDto.Address,
            Email = updateDto.Email,
            Name = updateDto.Name,
            Phone = updateDto.Phone
        };

        if (updateDto.CreatedAt != null)
        {
            customer.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customer.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customer;
    }
}
