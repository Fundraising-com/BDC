namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class direct_mail_template
    {
        [Key]
        [Column(Order = 0)]
        public int direct_mail_id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "text")]
        public string message { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(256)]
        public string image_url { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime create_date { get; set; }

        [StringLength(256)]
        public string document_path { get; set; }
    }
}
