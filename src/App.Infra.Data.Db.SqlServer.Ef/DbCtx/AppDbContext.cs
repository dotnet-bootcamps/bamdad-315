using App.Domain.Core.Products.Entities;
using App.Infra.Data.Db.SqlServer.Ef.EntitiesConfigs.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAttribute = App.Domain.Core.Products.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Serialization;

namespace App.Infra.Data.Db.SqlServer.Ef.DbCtx;
public class AppDbContext : IdentityDbContext<User,Role,int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<MyAttribute.Attribute> Attributes { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }

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
public class User : IdentityUser<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }


}

public class Role : IdentityRole<int>
{
    public string NameFa { get; set; }
}