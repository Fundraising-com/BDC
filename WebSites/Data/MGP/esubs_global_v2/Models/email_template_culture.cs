namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class email_template_culture
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int email_template_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string culture_code { get; set; }

        [Required]
        [StringLength(100)]
        public string subject { get; set; }

        [Required]
        [StringLength(8000)]
        public string body_html { get; set; }

        [Required]
        [StringLength(8000)]
        public string body_text { get; set; }

        [StringLength(1000)]
        public string footer_text { get; set; }

        [StringLength(2000)]
        public string footer_html { get; set; }

        public DateTime create_date { get; set; }

        public virtual culture culture { get; set; }

        public virtual email_template email_template { get; set; }
    }
}
