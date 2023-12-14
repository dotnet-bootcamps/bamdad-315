using App.Domain.Core.Products.AppServices;
using App.Domain.Core.Products.DTOs;
using App.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading;

namespace App.EndPoints.MVC.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductAppService _productAppService;

    public HomeController(ILogger<HomeController> logger , IProductAppService productAppService)
    {
        _logger = logger;
        _productAppService = productAppService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index(int? id, CancellationToken cancellationToken)
    {
        if (id != null)
        {
            var products = await _productAppService.GetProducts(id??0, cancellationToken);

            var c = id;
            return View(products);
        }
        else
        {
            try
            {
                var products = await _productAppService.GetProducts(cancellationToken);
                return View(products);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


    }
    [Authorize(Roles = "Admin")]
    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
