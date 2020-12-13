using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkManagement.Core.Models;

namespace MilkManagement.Data.Configurations
{
   public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Number).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedDate).IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.Property(x => x.Price).HasColumnType("decimal(5,4)");
            builder.ToTable(Constant.TblProduct, Constant.SchemaCore);
        }
    }
}
