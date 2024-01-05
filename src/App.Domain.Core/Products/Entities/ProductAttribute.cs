namespace App.Domain.Core.Products.Entities;
public class ProductAttribute
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AttributeId { get; set; }
    public string Value { get; set; }

    public Product Product { get; set; }
    public Attribute Attribute { get; set; }
}
