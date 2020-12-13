using System;
using MilkManagement.Core.Entities;
using MilkManagement.Core.Specifications.Base;

namespace MilkManagement.Core.Specifications
{
    public sealed class CategoryWithProductsSpecification : BaseSpecification<Category>
    {
        public CategoryWithProductsSpecification(Guid categoryId)
            : base(b => b.Id == categoryId)
        {
            AddInclude(b => b.Products);
         //   AddInclude($"{nameof(Category.Products)}.{nameof(Recipe.Ingredients)}");
        }
    }
}
