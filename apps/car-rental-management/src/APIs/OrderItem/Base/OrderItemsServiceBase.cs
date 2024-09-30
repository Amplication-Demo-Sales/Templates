using CarRentalManagementMobile.APIs;
using CarRentalManagementMobile.APIs.Common;
using CarRentalManagementMobile.APIs.Dtos;
using CarRentalManagementMobile.APIs.Errors;
using CarRentalManagementMobile.APIs.Extensions;
using CarRentalManagementMobile.Infrastructure;
using CarRentalManagementMobile.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementMobile.APIs;

public abstract class OrderItemsServiceBase : IOrderItemsService
{
    protected readonly CarRentalManagementMobileDbContext _context;

    public OrderItemsServiceBase(CarRentalManagementMobileDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one OrderItem
    /// </summary>
    public async Task<OrderItem> CreateOrderItem(OrderItemCreateInput createDto)
    {
        var orderItem = new OrderItemDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Price = createDto.Price,
            Quantity = createDto.Quantity,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            orderItem.Id = createDto.Id;
        }
        if (createDto.Orders != null)
        {
            orderItem.Orders = await _context
                .Orders.Where(order => createDto.Orders.Select(t => t.Id).Contains(order.Id))
                .ToListAsync();
        }

        if (createDto.Product != null)
        {
            orderItem.Product = await _context
                .Products.Where(product => createDto.Product.Id == product.Id)
                .FirstOrDefaultAsync();
        }

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OrderItemDbModel>(orderItem.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one OrderItem
    /// </summary>
    public async Task DeleteOrderItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context.OrderItems.FindAsync(uniqueId.Id);
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    public async Task<List<OrderItem>> OrderItems(OrderItemFindManyArgs findManyArgs)
    {
        var orderItems = await _context
            .OrderItems.Include(x => x.Product)
            .Include(x => x.Orders)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orderItems.ConvertAll(orderItem => orderItem.ToDto());
    }

    /// <summary>
    /// Meta data about OrderItem records
    /// </summary>
    public async Task<MetadataDto> OrderItemsMeta(OrderItemFindManyArgs findManyArgs)
    {
        var count = await _context.OrderItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one OrderItem
    /// </summary>
    public async Task<OrderItem> OrderItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItems = await this.OrderItems(
            new OrderItemFindManyArgs { Where = new OrderItemWhereInput { Id = uniqueId.Id } }
        );
        var orderItem = orderItems.FirstOrDefault();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        return orderItem;
    }

    /// <summary>
    /// Update one OrderItem
    /// </summary>
    public async Task UpdateOrderItem(
        OrderItemWhereUniqueInput uniqueId,
        OrderItemUpdateInput updateDto
    )
    {
        var orderItem = updateDto.ToModel(uniqueId);

        if (updateDto.Orders != null)
        {
            orderItem.Orders = await _context
                .Orders.Where(order => updateDto.Orders.Select(t => t).Contains(order.Id))
                .ToListAsync();
        }

        if (updateDto.Product != null)
        {
            orderItem.Product = await _context
                .Products.Where(product => updateDto.Product == product.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(orderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.OrderItems.Any(e => e.Id == orderItem.Id))
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
    /// Connect multiple Orders records to OrderItem
    /// </summary>
    public async Task ConnectOrders(
        OrderItemWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .OrderItems.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Orders);

        foreach (var child in childrenToConnect)
        {
            parent.Orders.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Orders records from OrderItem
    /// </summary>
    public async Task DisconnectOrders(
        OrderItemWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .OrderItems.Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Orders?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Orders records for OrderItem
    /// </summary>
    public async Task<List<Order>> FindOrders(
        OrderItemWhereUniqueInput uniqueId,
        OrderFindManyArgs orderItemFindManyArgs
    )
    {
        var orders = await _context
            .Orders.Where(m => m.OrderItemId == uniqueId.Id)
            .ApplyWhere(orderItemFindManyArgs.Where)
            .ApplySkip(orderItemFindManyArgs.Skip)
            .ApplyTake(orderItemFindManyArgs.Take)
            .ApplyOrderBy(orderItemFindManyArgs.SortBy)
            .ToListAsync();

        return orders.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Orders records for OrderItem
    /// </summary>
    public async Task UpdateOrders(
        OrderItemWhereUniqueInput uniqueId,
        OrderWhereUniqueInput[] childrenIds
    )
    {
        var orderItem = await _context
            .OrderItems.Include(t => t.Orders)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Orders.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        orderItem.Orders = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a Product record for OrderItem
    /// </summary>
    public async Task<Product> GetProduct(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context
            .OrderItems.Where(orderItem => orderItem.Id == uniqueId.Id)
            .Include(orderItem => orderItem.Product)
            .FirstOrDefaultAsync();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }
        return orderItem.Product.ToDto();
    }
}
