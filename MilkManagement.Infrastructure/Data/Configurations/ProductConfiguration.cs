using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkManagement.Core.Entities;
using MilkManagement.Infrastructure.Common;

namespace MilkManagement.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(Constant.TblProduct, Constant.SchemaCore);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Number).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).IsRequired().HasDefaultValueSql(Constant.SqlgetDate);
            builder.Property(x => x.UpdatedDate).IsRequired().HasComputedColumnSql(Constant.SqlgetDate);
        }
    }
}
