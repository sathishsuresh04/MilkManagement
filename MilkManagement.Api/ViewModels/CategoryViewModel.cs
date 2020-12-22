using System.Collections.Generic;
using MilkManagement.Core.Entities;

namespace MilkManagement.Api.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public ICollection<Product> Products { get; private set; }
    }
}
