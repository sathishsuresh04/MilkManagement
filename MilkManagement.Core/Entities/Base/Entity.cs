using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilkManagement.Core.Entities.Base
{
    public abstract class Entity : EntityBase<Guid>
    {
        [Column(Order = 104)]
        [Required]
        public Guid CreatedBy { get; set; }
        [Column(Order = 103)]
        [Required]
        public Guid UpdatedBy { get; set; }
        [Column(Order = 101)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }
        [Column(Order = 102)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; }
    }
}
