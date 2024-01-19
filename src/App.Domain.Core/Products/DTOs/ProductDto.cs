using System.ComponentModel.DataAnnotations;

namespace App.Domain.Core.Products.DTOs;


public class ProductDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "مقدار فیلد نام محصول اجباری می باشد.")]
    public string Name { get; set; }


    [Range(10000, 1000000, ErrorMessage = "مقدار قیمت حداقل 10 هزار تومان و نهایتا 1000000 تومان می باشد")]
    public long Price { get; set; } = 0;

    public int ProductCategoryId { get; set; }
    public string ProductCategoryTitle { get; set; }
    public string? Description { get; set; }
    public List<ProductAttributeDto> Attributes { get; set; }
}
