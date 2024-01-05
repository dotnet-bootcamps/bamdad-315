using App.Domain.Core.Products.Data.Repositories;
using App.Domain.Core.Products.DTOs;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.Products;
public class ProductQueryRepository : IProductQueryRepository
{
    private readonly AppDbContext _context;

    public ProductQueryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ProductDto?> GetProduct(int productId, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking().Where(p => p.Id == productId).Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ProductCategoryId = p.ProductCategoryId,
            ProductCategoryTitle = p.ProductCategory.Title
        }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ProductCategoryDto>> GetCategories(CancellationToken cancellationToken)
    {
        return await _context.ProductCategories.AsNoTracking().Select(p=>new ProductCategoryDto 
        { 
            Id = p.Id,
            Title=p.Title,
        }).ToListAsync(cancellationToken);
    }

    public async Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken)
    {
        return await _context.Products.Where(p => p.IsActive == true).AsNoTracking().Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ProductCategoryId = p.ProductCategoryId,
            ProductCategoryTitle = p.ProductCategory.Title
        }).ToListAsync(cancellationToken);
    }

    public async Task<List<ProductDto>?> GetProducts(int productCategoryId, CancellationToken cancellationToken)
    {
        return await _context.Products.AsNoTracking().Where(p => p.ProductCategoryId == productCategoryId).Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ProductCategoryId = p.ProductCategoryId
        }).ToListAsync(cancellationToken);
    }

    public async Task<List<AttributeDto>> GetCategoryAttributes(int CategoryId, CancellationToken cancellationToken)
    {
        var productCategory = await _context.ProductCategories.AsNoTracking().Include(p => p.Attributes).Where(p => p.Id == CategoryId).SingleAsync(cancellationToken);
        return productCategory.Attributes.Select(p => new AttributeDto
        {
            Id= p.Id,
            Title=p.Title,
        }).ToList();
    }
}
