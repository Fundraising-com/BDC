namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Message
    {
        [Key]
        public int Message_ID { get; set; }

        public int Participant_ID { get; set; }

        public bool Is_Read { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date_Sent { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date_Received { get; set; }

        [StringLength(50)]
        public string From_Name { get; set; }

        [StringLength(50)]
        public string From_Email { get; set; }

        [StringLength(50)]
        public string To_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string To_Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Subject { get; set; }

        [Column(TypeName = "text")]
        public string Body { get; set; }

        [Required]
        [StringLength(20)]
        public string Content_Type { get; set; }

        public virtual EFO_Participant EFO_Participant { get; set; }
    }
}
