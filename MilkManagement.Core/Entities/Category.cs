using System.Collections.Generic;
using MilkManagement.Core.Entities.Base;

namespace MilkManagement.Core.Entities
{
    public class Category:Entity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public  bool? Active { get; set; }
        public ICollection<Product> Products { get; private set; }

    }
}
