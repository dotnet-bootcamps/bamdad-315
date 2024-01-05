namespace App.Domain.Core.Products.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Price { get; set; }
    public int ProductCategoryId { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public DateTime? DeleteAt { get; set; }
    public int? AspNetUserId { get; set; }

    public ProductCategory ProductCategory { get; set; }
    public List<ProductAttribute> ProductAttributes { get; set; }
}
