using Microsoft.EntityFrameworkCore;
using ReservationManagement.APIs;
using ReservationManagement.APIs.Common;
using ReservationManagement.APIs.Dtos;
using ReservationManagement.APIs.Errors;
using ReservationManagement.APIs.Extensions;
using ReservationManagement.Infrastructure;
using ReservationManagement.Infrastructure.Models;

namespace ReservationManagement.APIs;

public abstract class ReviewsServiceBase : IReviewsService
{
    protected readonly ReservationManagementDbContext _context;

    public ReviewsServiceBase(ReservationManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Review
    /// </summary>
    public async Task<Review> CreateReview(ReviewCreateInput createDto)
    {
        var review = new ReviewDbModel
        {
            Comment = createDto.Comment,
            CreatedAt = createDto.CreatedAt,
            Date = createDto.Date,
            Rating = createDto.Rating,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            review.Id = createDto.Id;
        }
        if (createDto.Reservation != null)
        {
            review.Reservation = await _context
                .Reservations.Where(reservation => createDto.Reservation.Id == reservation.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.User != null)
        {
            review.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ReviewDbModel>(review.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Review
    /// </summary>
    public async Task DeleteReview(ReviewWhereUniqueInput uniqueId)
    {
        var review = await _context.Reviews.FindAsync(uniqueId.Id);
        if (review == null)
        {
            throw new NotFoundException();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Reviews
    /// </summary>
    public async Task<List<Review>> Reviews(ReviewFindManyArgs findManyArgs)
    {
        var reviews = await _context
            .Reviews.Include(x => x.Reservation)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return reviews.ConvertAll(review => review.ToDto());
    }

    /// <summary>
    /// Meta data about Review records
    /// </summary>
    public async Task<MetadataDto> ReviewsMeta(ReviewFindManyArgs findManyArgs)
    {
        var count = await _context.Reviews.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Review
    /// </summary>
    public async Task<Review> Review(ReviewWhereUniqueInput uniqueId)
    {
        var reviews = await this.Reviews(
            new ReviewFindManyArgs { Where = new ReviewWhereInput { Id = uniqueId.Id } }
        );
        var review = reviews.FirstOrDefault();
        if (review == null)
        {
            throw new NotFoundException();
        }

        return review;
    }

    /// <summary>
    /// Update one Review
    /// </summary>
    public async Task UpdateReview(ReviewWhereUniqueInput uniqueId, ReviewUpdateInput updateDto)
    {
        var review = updateDto.ToModel(uniqueId);

        if (updateDto.Reservation != null)
        {
            review.Reservation = await _context
                .Reservations.Where(reservation => updateDto.Reservation == reservation.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.User != null)
        {
            review.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(review).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reviews.Any(e => e.Id == review.Id))
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
    /// Get a Reservation record for Review
    /// </summary>
    public async Task<Reservation> GetReservation(ReviewWhereUniqueInput uniqueId)
    {
        var review = await _context
            .Reviews.Where(review => review.Id == uniqueId.Id)
            .Include(review => review.Reservation)
            .FirstOrDefaultAsync();
        if (review == null)
        {
            throw new NotFoundException();
        }
        return review.Reservation.ToDto();
    }

    /// <summary>
    /// Get a User record for Review
    /// </summary>
    public async Task<User> GetUser(ReviewWhereUniqueInput uniqueId)
    {
        var review = await _context
            .Reviews.Where(review => review.Id == uniqueId.Id)
            .Include(review => review.User)
            .FirstOrDefaultAsync();
        if (review == null)
        {
            throw new NotFoundException();
        }
        return review.User.ToDto();
    }
}
