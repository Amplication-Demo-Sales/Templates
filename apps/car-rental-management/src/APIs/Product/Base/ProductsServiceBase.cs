using CarRentalManagement.APIs;
using CarRentalManagement.APIs.Common;
using CarRentalManagement.APIs.Dtos;
using CarRentalManagement.APIs.Errors;
using CarRentalManagement.APIs.Extensions;
using CarRentalManagement.Infrastructure;
using CarRentalManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly CarRentalManagementDbContext _context;

    public ProductsServiceBase(CarRentalManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    public async Task<Product> CreateProduct(ProductCreateInput createDto)
    {
        var product = new ProductDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            Price = createDto.Price,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            product.Id = createDto.Id;
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProductDbModel>(product.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    public async Task DeleteProduct(ProductWhereUniqueInput uniqueId)
    {
        var product = await _context.Products.FindAsync(uniqueId.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    public async Task<List<Product>> Products(ProductFindManyArgs findManyArgs)
    {
        var products = await _context
            .Products.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return products.ConvertAll(product => product.ToDto());
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    public async Task<MetadataDto> ProductsMeta(ProductFindManyArgs findManyArgs)
    {
        var count = await _context.Products.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    public async Task<Product> Product(ProductWhereUniqueInput uniqueId)
    {
        var products = await this.Products(
            new ProductFindManyArgs { Where = new ProductWhereInput { Id = uniqueId.Id } }
        );
        var product = products.FirstOrDefault();
        if (product == null)
        {
            throw new NotFoundException();
        }

        return product;
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    public async Task UpdateProduct(ProductWhereUniqueInput uniqueId, ProductUpdateInput updateDto)
    {
        var product = updateDto.ToModel(uniqueId);

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == product.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
