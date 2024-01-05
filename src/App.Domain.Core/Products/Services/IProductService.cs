using App.Domain.Core.Products.DTOs;

namespace App.Domain.Core.Products.Services;
public interface IProductService
{
    public Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken);
    public Task<List<ProductDto>?> GetProducts(int categoryId, CancellationToken cancellationToken);
    public Task<ProductDto> GetProduct(int productId, CancellationToken cancellationToken);
    public Task AddProduct(ProductDto product, CancellationToken cancellationToken);
    public Task UpdateProduct(ProductDto product, CancellationToken cancellationToken);
    public Task DeleteProduct(int productId, CancellationToken cancellationToken);
    Task<List<ProductCategoryDto>> GetCategories(CancellationToken cancellationToken);
    Task<List<AttributeDto>> GetCategoryAttributes(int CategoryId, CancellationToken cancellationToken);
}
