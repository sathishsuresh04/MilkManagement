using System.Collections.Generic;

namespace MilkManagement.Core.Models
{
   public  class ProductArea: BaseEntity
    {
        public  string Name { get; set; }
        public bool? Active { get; set; }
        public List<Product>Products { get; set; }
    }
}
