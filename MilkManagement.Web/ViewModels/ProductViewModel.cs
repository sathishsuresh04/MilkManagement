using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilkManagement.Web.ViewModels
{
    public class ProductViewModel: BaseViewModel
    {
        public string ProductName { get; set; }
        public string Number { get; set; }
        public string Unit { get; set; }//kg or ml or packet or styck
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool? Active { get; set; }
        public string ProductReason { get; set; }
        public Guid? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }

      //  public string Name { get; set; }
    }
}
