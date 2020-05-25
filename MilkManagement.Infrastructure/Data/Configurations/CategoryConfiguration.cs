using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MilkManagement.Core.Entities;
using MilkManagement.Infrastructure.Common;

namespace MilkManagement.Infrastructure.Data.Configurations
{
   public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(Constant.TblCategory, Constant.SchemaCore);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.CreatedDate).IsRequired();//.ValueGeneratedOnAdd();
            builder.Property(x => x.UpdatedDate).IsRequired();//.ValueGeneratedOnAddOrUpdate();
         
        }
    }
}
