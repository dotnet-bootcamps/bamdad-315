using App.Domain.Core.Products.Data.Repositories;
using App.Domain.Core.Products.DTOs;
using App.Domain.Core.Products.Entities;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.Products;
public class ProductCommandRepository : IProductCommandRepository
{
    private readonly AppDbContext _context;

    public ProductCommandRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task Add(ProductDto product, CancellationToken cancellationToken)
    {
        Product newProduct = new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ProductCategoryId = product.ProductCategoryId
        };

        await _context.Products.AddAsync(newProduct,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int productId, CancellationToken cancellationToken)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == productId);
        if (product != null) 
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(ProductDto product, CancellationToken cancellationToken)
    {
        var oldproduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken);
        if(oldproduct != null)
        {
            oldproduct.Name = product.Name;
            oldproduct.Price = product.Price;
            oldproduct.ProductCategoryId = product.ProductCategoryId;
            oldproduct.Description = product.Description;
        }
        await _context.SaveChangesAsync(cancellationToken);
        
    }
}
