namespace App.Domain.Core.Products.Entities;
public class Attribute
{
    public int Id { get; set; }
    public string Title { get; set; }

    public List<ProductAttribute> ProductAttributes { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
}
