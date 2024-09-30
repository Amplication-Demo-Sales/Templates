using CrmManagement.APIs.Dtos;
using CrmManagement.Infrastructure.Models;

namespace CrmManagement.APIs.Extensions;

public static class LeadsExtensions
{
    public static Lead ToDto(this LeadDbModel model)
    {
        return new Lead
        {
            Activities = model.Activities?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Email = model.Email,
            Id = model.Id,
            Name = model.Name,
            Source = model.Source,
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static LeadDbModel ToModel(this LeadUpdateInput updateDto, LeadWhereUniqueInput uniqueId)
    {
        var lead = new LeadDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Name = updateDto.Name,
            Source = updateDto.Source,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            lead.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            lead.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            lead.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return lead;
    }
}
