using App.Domain.Core.Products.AppServices;
using App.Domain.Core.Products.DTOs;
using App.EndPoints.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.AspNetCore.Identity;
using System;
//using ExchangeProxy;
namespace App.EndPoints.MVC.Controllers;

[Authorize]
public class HomeController : eShopBaseController
{
    private IMemoryCache _memoryCache;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<HomeController> _logger;
    private readonly AppSettings _appSettings;
    private readonly IConfiguration _configuration;

    private readonly IProductAppService _productAppService;
    //private readonly IExchageProxy _exchageProxy;

    public HomeController(ILogger<HomeController> logger , 
        AppSettings appSettings ,
        IConfiguration configuration,
        IProductAppService productAppService , IMemoryCache memoryCache,
        UserManager<User> userManager
        , RoleManager<Role> roleManager
            ,IWebHostEnvironment environment
        //,IExchageProxy exchageProxy
        )
    {
        _memoryCache = memoryCache;
        _userManager = userManager;
        _roleManager = roleManager;
        _environment = environment;
        _logger = logger;
        _appSettings = appSettings;
        _configuration = configuration;
        _productAppService = productAppService;
        //_exchageProxy = exchageProxy;
    }


    public IActionResult ModelBinding([FromQuery]int id)
    {
        return Ok();
    }

    [AllowAnonymous]
    public async Task SeedData()
    {

        if (_environment.IsDevelopment())
        {
            await _roleManager.CreateAsync(new Role
            {
                Name = "Admin",
                NameFa = "ادمین",
            });

            await _roleManager.CreateAsync(new Role
            {
                Name = "User",
                NameFa = "کاربر",
            });

            var adminUser = new User
            {
                UserName = "admin",
                FirstName = "admin",
                LastName = "bamdad",
                Email = "admin@bamdad.ir"
            };

            var result = await _userManager.CreateAsync(adminUser, "1234567");

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("Admin");
                var result2 = await _userManager.AddToRoleAsync(adminUser, role.Name);
            }
        }

    }


    [AllowAnonymous]
    public async Task<IActionResult> Index(int? id, CancellationToken cancellationToken)
    {

        var appName = _configuration["AppName"];
        var loggingDefaultLevel = _configuration["Logging:LogLevel:Default"];

        //User.Identity.GetUserId();


        if (User.Identity.IsAuthenticated)
        {

        }

        var userID = GetUserId();




        ViewBag.UserName = User.Identity.Name;

        var isLogin = User?.Identity?.IsAuthenticated;
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
                var CacheKey = "ChachedProduct";
                if (!_memoryCache.TryGetValue(CacheKey, out List<ProductDto> cacheValue))
                {

                    cacheValue = await _productAppService.GetProducts(cancellationToken); ;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(3));

                    _memoryCache.Set(CacheKey, cacheValue, cacheEntryOptions);
                }
                return View(cacheValue);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //return Created();
            //return Ok();
            //return Unauthorized();
            //return Forbid();
            //throw new Exception();

        }


    }

    public async Task<IActionResult> AddProduct()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDto model)
    {
        if (ModelState.IsValid)
        {

            // اضافه شد توی دیتابیس
            return RedirectToAction("Index");
        }

        return View(model);
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

    //[AllowAnonymous]
    //public async  Task<IActionResult> Students(int? id, CancellationToken cancellationToken)
    
    //{
    //    var students= await _exchageProxy.GetAllStudents(cancellationToken);
    //    //HttpClient client = new HttpClient();
    //    //client.DefaultRequestHeaders.Add("apikey", "ABCabc%40123");
    //    //var response = await client.GetAsync("https://localhost:7004/api/v1/Student/GetAll");
    //    //var result = await response.Content.ReadAsStringAsync(cancellationToken);
    //    //var resultObj = JsonConvert.DeserializeObject<List<Student>>(result);

    //    await _exchageProxy.SetStudent(students.First(), cancellationToken);

    //    return View(students);

    //}

    


}
