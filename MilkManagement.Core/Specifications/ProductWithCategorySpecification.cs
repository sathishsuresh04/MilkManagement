﻿using MilkManagement.Core.Entities;
using MilkManagement.Core.Specifications.Base;

namespace MilkManagement.Core.Specifications
{
  public sealed class ProductWithCategorySpecification:BaseSpecification<Product>
    {
        public ProductWithCategorySpecification(string productName)
            : base(p => p.Name.ToLower().Contains(productName.ToLower()))
        {
            AddInclude(p => p.Category);
        }
        public ProductWithCategorySpecification() : base(null)
        {
            AddInclude(p => p.Category);
        }
    }
}
