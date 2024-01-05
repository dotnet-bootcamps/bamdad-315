namespace App.Domain.Core.Products.Entities;
public class ProductCategory
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? ParentId { get; set; }
    public List<Product> Products { get; set; }
    public ProductCategory Parent { get; set; }
    public List<ProductCategory> Childs { get; set; }
    public List<Attribute> Attributes { get; set; }
}
