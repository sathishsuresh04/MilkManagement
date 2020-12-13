using System.ComponentModel.DataAnnotations.Schema;

namespace MilkManagement.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public bool? Active { get; set; }
        public string Unit { get; set; }//kg or ml or packet or styck
        public decimal? Price { get; set; }
        public string Comment { get; set; }
        public ProductArea ProductArea { get; set; }
    }
}
