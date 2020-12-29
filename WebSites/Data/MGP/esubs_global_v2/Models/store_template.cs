namespace GA.BDC.Data.MGP.esubs_global_v2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class store_template
    {
        public store_template()
        {
            partner_store_template = new HashSet<partner_store_template>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int store_template_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        public int? store_id { get; set; }

        public int aggregator_id { get; set; }

        public int? account_number { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        [StringLength(7)]
        public string subdivision_code { get; set; }

        public int? opportunity_id { get; set; }

        public virtual culture culture { get; set; }

        public virtual ICollection<partner_store_template> partner_store_template { get; set; }

        public virtual subdivision subdivision { get; set; }
    }
}
