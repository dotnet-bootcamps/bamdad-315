using App.Domain.AppServices.Products;
using App.Domain.Core.Products.AppServices;
using App.Domain.Core.Products.Data.Repositories;
using App.Domain.Core.Products.Services;
using App.Domain.Services.Products;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using App.Infra.Data.Repos.Ef.Products;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database=EshopDB;").LogTo(Console.WriteLine));
builder.Services.AddScoped<IProductAppService,ProductAppService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
