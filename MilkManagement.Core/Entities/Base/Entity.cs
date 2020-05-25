using System;

namespace MilkManagement.Core.Entities.Base
{
   public abstract class Entity:EntityBase<Guid>
    {
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
