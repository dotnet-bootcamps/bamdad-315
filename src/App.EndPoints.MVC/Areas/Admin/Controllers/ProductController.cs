using App.Domain.Core.Products.AppServices;
using App.Domain.Core.Products.DTOs;
using App.EndPoints.MVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace App.EndPoints.MVC.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IProductAppService _productAppService;
    private readonly IMemoryCache _memoryCache;

    public ProductController(IProductAppService productAppService,
        IMemoryCache memoryCache)
    {
        _productAppService = productAppService;
        _memoryCache = memoryCache;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        //if (_memoryCache[])
        //{
            
        //}
        var products = await _productAppService.GetProducts(cancellationToken);
        return View(products);
    }
    [HttpGet]    
    
    public async Task<IActionResult> AddProduct(CancellationToken cancellationToken)
    {
        var categories = await _productAppService.GetCategories(cancellationToken);
        return View(categories);
    }

    [HttpGet]
    public async Task<IActionResult> Add(int id , CancellationToken cancellationToken)
    {
        var attributes = await _productAppService.GetCategoryAttributes(id, cancellationToken);
        ViewBag.Attributes = attributes.Select(p => new AttributeViewModel()
        {
            Id = p.Id,
            Name = p.Title
        }).ToList();
        ViewBag.ProductCategoryId=id;



        return View();
    }
    [HttpPost]
    public IActionResult Add(ProductViewModel product , Dictionary<int, string> Attr)
    {

        return View(product);
    }
}
