using ReservationManagementMobile.APIs.Dtos;
using ReservationManagementMobile.Infrastructure.Models;

namespace ReservationManagementMobile.APIs.Extensions;

public static class PaymentsExtensions
{
    public static Payment ToDto(this PaymentDbModel model)
    {
        return new Payment
        {
            Amount = model.Amount,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            PaymentDate = model.PaymentDate,
            Reservation = model.ReservationId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PaymentDbModel ToModel(
        this PaymentUpdateInput updateDto,
        PaymentWhereUniqueInput uniqueId
    )
    {
        var payment = new PaymentDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            PaymentDate = updateDto.PaymentDate
        };

        if (updateDto.CreatedAt != null)
        {
            payment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Reservation != null)
        {
            payment.ReservationId = updateDto.Reservation;
        }
        if (updateDto.UpdatedAt != null)
        {
            payment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return payment;
    }
}
