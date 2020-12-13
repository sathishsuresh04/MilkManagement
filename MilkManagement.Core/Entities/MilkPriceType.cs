using MilkManagement.Core.Entities.Base;

namespace MilkManagement.Core.Entities
{
    public class MilkPriceType:Entity
    {
        public string Name { get; set; }
        //Fixed price, Fat, Fat/SNF , CLR/Fat
        public bool? Active { get; set; }
    }
}
