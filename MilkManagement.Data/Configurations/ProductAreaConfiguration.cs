using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkManagement.Core.Models;

namespace MilkManagement.Data.Configurations
{
   public class ProductAreaConfiguration:IEntityTypeConfiguration<ProductArea>
    {
        public void Configure(EntityTypeBuilder<ProductArea> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).HasColumnType("Date").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedDate).HasColumnType("Date").IsRequired().ValueGeneratedOnAddOrUpdate();
            builder.ToTable(Constant.TblProductArea, Constant.SchemaCore);
        }
    }
}
