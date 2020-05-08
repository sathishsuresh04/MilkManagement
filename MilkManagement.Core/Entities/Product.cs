using System;
using System.Collections.Generic;
using System.Text;
using MilkManagement.Core.Entities.Base;

namespace MilkManagement.Core.Entities
{
    public class Product:Entity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Unit { get; set; }//kg or ml or packet or styck
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
       // public short? UnitsOnOrder { get; set; }
      //  public short? ReorderLevel { get; set; }
        public bool Active { get; set; }
        public string ProductReason { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
