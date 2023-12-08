namespace App.Domain.Core.Products.DTOs;
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }
    public int ProductCategoryId { get; set; }
    public string? Description { get; set; }
}
