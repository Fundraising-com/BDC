using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
   [Table("notification")]
   public partial class notification
   {
      [Key]
      public int id { get; set; }
      [Required]
      public int external_id { get; set; }
      [Required]
      public int type { get; set; }
      [MaxLength(250)]
      public string email { get; set; }
      [MaxLength(1000)]
      public string extra_data { get; set; }
      [Required]
      public DateTime created_on { get; set; }
   }
}
