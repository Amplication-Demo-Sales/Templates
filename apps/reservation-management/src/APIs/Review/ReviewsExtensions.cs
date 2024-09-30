using ReservationManagement.APIs.Dtos;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs.Extensions;

public static class ReviewsExtensions
{
    public static Review ToDto(this ReviewDbModel model)
    {
        return new Review
        {
            Comment = model.Comment,
            CreatedAt = model.CreatedAt,
            Date = model.Date,
            Id = model.Id,
            Rating = model.Rating,
            Reservation = model.ReservationId,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static ReviewDbModel ToModel(
        this ReviewUpdateInput updateDto,
        ReviewWhereUniqueInput uniqueId
    )
    {
        var review = new ReviewDbModel
        {
            Id = uniqueId.Id,
            Comment = updateDto.Comment,
            Date = updateDto.Date,
            Rating = updateDto.Rating
        };

        if (updateDto.CreatedAt != null)
        {
            review.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Reservation != null)
        {
            review.ReservationId = updateDto.Reservation;
        }
        if (updateDto.UpdatedAt != null)
        {
            review.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            review.UserId = updateDto.User;
        }

        return review;
    }
}
