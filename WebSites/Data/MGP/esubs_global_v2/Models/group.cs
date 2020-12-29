namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
   using System;
   using System.Collections.Generic;
   using System.ComponentModel.DataAnnotations;
   using System.ComponentModel.DataAnnotations.Schema;
   using System.Data.Entity.Spatial;

   [Table("group")]
   public partial class group
   {
      public group()
      {
         event_group = new HashSet<event_group>();
         group1 = new HashSet<group>();
         group_group_status = new HashSet<group_group_status>();
         payment_info = new HashSet<payment_info>();
      }

      [Key]
      public int group_id { get; set; }

      public int? parent_group_id { get; set; }

      public int sponsor_id { get; set; }

      public int partner_id { get; set; }

      public int? lead_id { get; set; }

      [StringLength(20)]
      public string external_group_id { get; set; }

      [StringLength(200)]
      public string group_name { get; set; }

      public bool test_group { get; set; }

      public int? expected_membership { get; set; }

      [StringLength(255)]
      public string group_url { get; set; }

      [StringLength(255)]
      public string redirect { get; set; }

      public DateTime? event_date { get; set; }

      [StringLength(255)]
      public string image { get; set; }

      [StringLength(1024)]
      public string comments { get; set; }

      public DateTime create_date { get; set; }

      public virtual ICollection<event_group> event_group { get; set; }

      public virtual ICollection<group> group1 { get; set; }

      public virtual group group2 { get; set; }

      public virtual ICollection<group_group_status> group_group_status { get; set; }

      public virtual member_hierarchy member_hierarchy { get; set; }

      public virtual ICollection<payment_info> payment_info { get; set; }
   }
}
