using App.Domain.Core.Products.DTOs;

namespace App.EndPoints.MVC.Areas.Admin.Models;

public class ProductViewModel
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public long? Price { get; set; }
    public int? ProductCategoryId { get; set; }
    public string? Description { get; set; }

}
