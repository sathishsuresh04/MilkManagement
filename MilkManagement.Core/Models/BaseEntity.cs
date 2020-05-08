using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MilkManagement.Core.Models
{
  public  class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public  Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        public  DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
