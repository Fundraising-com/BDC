using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotationsExtensions;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
   [Table("review")]
   public partial class review
   {
      [Key]
      public int Id { get; set; }
      [Required]
      [StringLength(30)]
      public string Name { get; set; }
      [Required]
      [StringLength(500)]
      public string Comments { get; set; }
      [Min(1)]
      [Max(5)]
      public int Rating { get; set; }
      [Required]
      public int ProductId { get; set; }

      public int? SaleId { get; set; }

      public bool IsApproved { get; set; }

      public DateTime Created { get; set; }
      [Required]
      [MaxLength(255)]
      public string Email { get; set; }
   }
}
