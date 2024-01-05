using App.Domain.Core.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Db.SqlServer.Ef.EntitiesConfigs.Products;
public class ProductAttributeEntityConfigs : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.ToTable("ProductAttributes", "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Value).HasMaxLength(500);
   
    }
}
