using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkManagement.Core.Entities;
using MilkManagement.Infrastructure.Common;

namespace MilkManagement.Infrastructure.Data.Configurations
{
   public class MilkPriceTypeConfiguration:IEntityTypeConfiguration<MilkPriceType>
    {
        public void Configure(EntityTypeBuilder<MilkPriceType> builder)
        {
            builder.ToTable(Constant.TblMilkPriceType, Constant.SchemaCore);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired();
     
        }
    }
}
