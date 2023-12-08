using App.Domain.Core.Products.DTOs;

namespace App.Domain.Core.Products.Data.Repositories;
public interface IProductCommandRepository
{
    public Task Add(ProductDto product,CancellationToken cancellationToken);
    public Task Update(ProductDto product, CancellationToken cancellationToken);
    public Task Delete(int productId, CancellationToken cancellationToken);
}
