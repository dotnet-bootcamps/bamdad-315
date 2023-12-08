using App.Domain.Core.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace App.Infra.Data.Db.SqlServer.Ef.EntitiesConfigs.Products;
public class ProductEntityConfigs : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500);
        builder.Property(x=>x.Description).HasMaxLength(4000);


    }
}
