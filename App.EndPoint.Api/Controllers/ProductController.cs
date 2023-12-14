using App.Domain.Core.Products.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = await _productAppService.GetProducts(cancellationToken);
        return Ok(products);
    }
}
