using App.Domain.Core.Products.DTOs;

namespace App.Domain.Core.Products.Data.Repositories;
public interface IProductQueryRepository
{
    public Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken);
    public Task<List<ProductDto>?> GetProducts(int productCategoryId, CancellationToken cancellationToken);
    public Task<ProductDto?> GetProduct(int productId, CancellationToken cancellationToken);
    Task<List<ProductCategoryDto>> GetCategories(CancellationToken cancellationToken);
    Task<List<AttributeDto>> GetCategoryAttributes(int CategoryId, CancellationToken cancellationToken);

}
