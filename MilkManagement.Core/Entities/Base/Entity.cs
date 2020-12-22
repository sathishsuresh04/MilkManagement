using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkManagement.Core.Entities.Base
{
    public abstract class Entity : EntityBase<Guid>
    {
        [Column(Order = 104)]
        public Guid CreatedBy { get; set; }
        [Column(Order = 103)]
        public Guid UpdatedBy { get; set; }
        [Column(Order = 101)]
        public DateTime CreatedDate { get; set; }
        [Column(Order = 102)]
        public DateTime UpdatedDate { get; set; }
    }
}
