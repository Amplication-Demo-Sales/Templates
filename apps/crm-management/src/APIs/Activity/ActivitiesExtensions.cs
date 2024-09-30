using CrmManagement.APIs.Dtos;
using CrmManagement.Infrastructure.Models;

namespace CrmManagement.APIs.Extensions;

public static class ActivitiesExtensions
{
    public static Activity ToDto(this ActivityDbModel model)
    {
        return new Activity
        {
            ActivityDate = model.ActivityDate,
            Contact = model.ContactId,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            Description = model.Description,
            Id = model.Id,
            Lead = model.LeadId,
            Opportunity = model.OpportunityId,
            RelatedTo = model.RelatedTo,
            Subject = model.Subject,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ActivityDbModel ToModel(
        this ActivityUpdateInput updateDto,
        ActivityWhereUniqueInput uniqueId
    )
    {
        var activity = new ActivityDbModel
        {
            Id = uniqueId.Id,
            ActivityDate = updateDto.ActivityDate,
            Description = updateDto.Description,
            RelatedTo = updateDto.RelatedTo,
            Subject = updateDto.Subject
        };

        if (updateDto.Contact != null)
        {
            activity.ContactId = updateDto.Contact;
        }
        if (updateDto.CreatedAt != null)
        {
            activity.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            activity.CustomerId = updateDto.Customer;
        }
        if (updateDto.Lead != null)
        {
            activity.LeadId = updateDto.Lead;
        }
        if (updateDto.Opportunity != null)
        {
            activity.OpportunityId = updateDto.Opportunity;
        }
        if (updateDto.UpdatedAt != null)
        {
            activity.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return activity;
    }
}
