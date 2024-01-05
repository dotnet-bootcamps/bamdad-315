namespace App.Domain.Core.Products.DTOs;
public class ProductAttributeDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int AttributeId { get; set; }
    public string AttributeTitle { get; set; }
    public string Value { get; set; }
}
