using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkManagement.Core.Models;

namespace MilkManagement.Data.Configurations
{
   public class MilkPriceTypeConfiguration:IEntityTypeConfiguration<MilkPriceType>
    {
        public void Configure(EntityTypeBuilder<MilkPriceType> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired();
            builder.ToTable(Constant.TblMilkPriceType, Constant.SchemaCore);
        }
    }
}
