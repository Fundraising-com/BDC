using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
   [Table("landingPage")]
   public partial class landingPage
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int id { get; set; }

      [Required]
      public int partner_id { get; set; }
      [Required]
      public int group_id { get; set; }
      [StringLength(100)]
      public string side_bar_image { get; set; }
      [StringLength(100)]
      public string donate_image { get; set; }
      [StringLength(100)]
      public string raise_funds_image { get; set; }
      public string description { get; set; }
      [Required]
      public decimal goal { get; set; }
      [StringLength(255)]
      public string image_top { get; set; }

      public bool show_find_groups { get; set; }
      public bool show_featured_groups { get; set; }
      public bool show_top_participants { get; set; }
      public bool show_create_fundraising { get; set; }
      public bool show_image_motivator { get; set; }
      public string learn_more_text { get; set; }
      public bool apply_partner_percent_on_total_amount { get; set; }
   }
}
