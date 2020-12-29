using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
   public class touch_email_process_queue
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int id { get; set; }

      [Required]
      public int event_id { get; set; }

      [Required]
      [StringLength(100)]
      public string email_address { get; set; }

      [StringLength(100)]
      public string first_name { get; set; }

      [StringLength(100)]
      public string last_name { get; set; }
      
      public int? participation_channel_id { get; set; }

      [Required]
      public int bussiness_rule_id { get; set; }

      [StringLength(4000)]
      public string custom_message { get; set; }

      [Required]
      public short status { get; set; }

      [Required]
      public DateTime created { get; set; }

      [Required]
      public bool is_sponsor { get; set; }

      [Required]
      public int sponsor_event_participation_id { get; set; }

      [Required]
      public int partner_id { get; set; }

      [Required]
      [StringLength(100)]
      public string subject { get; set; }

      [Required]
      public int reminder_recurrency { get; set; }

      [Required]
      public int creation_channel_id { get; set; }

      [Required]
      public int email_flow_id { get; set; }

      [Required]
      public int message_type { get; set; }

      [Required]
      public int event_type { get; set; }
   }

   public enum touch_email_process_queue_status
   {
      Invalid = 0,
      Scheduled = 1,
      Flagged = 2
   }
}
