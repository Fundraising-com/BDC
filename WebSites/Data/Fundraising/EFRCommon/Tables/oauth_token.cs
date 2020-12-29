using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
   [Table("oauth_token")]
   public partial class oauth_token
   {
      [Key]
      public string id { get; set; }
      [Required]
      [MaxLength(50)]
      public string subject { get; set; }
      [Required]
      [MaxLength(50)]
      public string client_id { get; set; }
      public DateTime issued_utc { get; set; }
      public DateTime expires_utc { get; set; }
      [Required]
      public string protected_ticket { get; set; }
   }
}
