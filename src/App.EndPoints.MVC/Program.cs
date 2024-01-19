using App.Domain.AppServices.Products;
using App.Domain.Core.Products.AppServices;
using App.Domain.Core.Products.Data.Repositories;
using App.Domain.Core.Products.Services;
using App.Domain.Services.Products;
using App.EndPoints.MVC.Models;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using App.Infra.Data.Repos.Ef.Products;
using Microsoft.EntityFrameworkCore;
//using ExchangeProxy;
//using ExchangeProxy.Models;

var builder = WebApplication.CreateBuilder(args);


// configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    //.AddJsonFile($"appsettings-logging.{builder.Environment.EnvironmentName}.json")
    //.AddIniFile()
    //.AddXmlFile()
    //.AddCommandLine()
    //.AddEnvironmentVariables()
    //.AddUserSecrets("f55ef90e-093a-42f0-85ab-d33b28d52cbd")
    ;

var appName = builder.Configuration["AppName"];
var loggingDefaultLevel = builder.Configuration["Logging:LogLevel:Default"];


//var appSettings = builder.Configuration.Get<AppSettings>();

builder.Services.AddSingleton(builder.Configuration.Get<AppSettings>());


var appDbConnectionString = builder.Configuration.GetConnectionString("AppDb");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database=BamdadShopDb2;").LogTo(Console.WriteLine));
builder.Services.AddIdentity<User, Role>(option =>
{
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequiredLength = 6;
    option.Password.RequiredUniqueChars = 2;
}
).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddScoped<IProductAppService, ProductAppService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
//builder.Services.Add_ExchangeProxy(new ExchangeOption()
//{
//    ApiKey = "ABCabc@123"
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//if(app.Environment.IsEnvironment("Mahmoud-Pc"))
//{

//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
