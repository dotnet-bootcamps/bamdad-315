using App.Domain.Core.Products.Entities;
using App.Infra.Data.Db.SqlServer.Ef.EntitiesConfigs.Products;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace App.Infra.Data.Db.SqlServer.Ef.DbCtx;
public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration(new ProductEntityConfigs());
        //modelBuilder.ApplyConfiguration(new ProductCategoryEntityConfigs());
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        modelBuilder.Entity<Product>().HasQueryFilter(a => a.IsDelete ==false);
    }
}
