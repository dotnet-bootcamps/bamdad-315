using App.Domain.Core.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Db.SqlServer.Ef.EntitiesConfigs.Products;
public class ProductCategoryEntityConfigs : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories", "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(100);
        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Childs)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
            
    }
}
