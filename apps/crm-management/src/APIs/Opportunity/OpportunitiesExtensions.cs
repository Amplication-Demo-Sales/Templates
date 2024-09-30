using CrmManagement.APIs.Dtos;
using CrmManagement.Infrastructure.Models;

namespace CrmManagement.APIs.Extensions;

public static class OpportunitiesExtensions
{
    public static Opportunity ToDto(this OpportunityDbModel model)
    {
        return new Opportunity
        {
            Activities = model.Activities?.Select(x => x.Id).ToList(),
            Amount = model.Amount,
            CloseDate = model.CloseDate,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Id = model.Id,
            Name = model.Name,
            Stage = model.Stage,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OpportunityDbModel ToModel(
        this OpportunityUpdateInput updateDto,
        OpportunityWhereUniqueInput uniqueId
    )
    {
        var opportunity = new OpportunityDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            CloseDate = updateDto.CloseDate,
            Name = updateDto.Name,
            Stage = updateDto.Stage
        };

        if (updateDto.CreatedAt != null)
        {
            opportunity.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            opportunity.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            opportunity.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return opportunity;
    }
}
