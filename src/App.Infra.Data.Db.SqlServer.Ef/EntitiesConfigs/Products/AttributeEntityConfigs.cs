using App.Domain.Core.Products.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Serialization;
using MyAttribute = App.Domain.Core.Products.Entities;

namespace App.Infra.Data.Db.SqlServer.Ef.EntitiesConfigs.Products;
public class AttributeEntityConfigs : IEntityTypeConfiguration<MyAttribute.Attribute>
{
    public void Configure(EntityTypeBuilder<MyAttribute.Attribute> builder)
    {
        builder.ToTable("Attributes", "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(500);
    }
}
