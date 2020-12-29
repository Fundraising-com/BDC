namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class direct_mail_info
    {
        [Key]
        [Column(Order = 0)]
        public int direct_mail_info_id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "text")]
        public string message { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string image_url { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool moderated { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short direct_mail_status { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime create_date { get; set; }

        [StringLength(256)]
        public string document_path { get; set; }

        public int? event_participation_id { get; set; }

        [StringLength(10)]
        public string member_hierarchy_id { get; set; }
    }
}
