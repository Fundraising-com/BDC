using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
   [Table("oauth_client")]
   public partial class oauth_client
   {
      [Key]
      public string id { get; set; }
      [Required]
      public string secret { get; set; }
      [Required]
      [MaxLength(100)]
      public string name { get; set; }
      public int application_type { get; set; }
      public bool is_active { get; set; }
      public int refresh_token_life_time { get; set; }
      [MaxLength(100)]
      public string allowed_origin { get; set; }
   }
}
