namespace MilkManagement.Core.Models
{
  public  class MilkPriceType:BaseEntity
    {
        public string Name { get; set; }
        //Fixed price, Fat, Fat/SNF , CLR/Fat
        public bool? Active { get; set; }
    }
}
