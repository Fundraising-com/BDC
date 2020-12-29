namespace GA.BDC.Data.MGP.fastfundraising.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FC")]
    public partial class FC
    {
        public FC()
        {
            fc_testimonial = new HashSet<fc_testimonial>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int ext_id { get; set; }

        [StringLength(50)]
        public string email_address { get; set; }

        public short active { get; set; }

        [StringLength(25)]
        public string login { get; set; }

        [StringLength(50)]
        public string url { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        [StringLength(50)]
        public string image_url { get; set; }

        [StringLength(50)]
        public string phone { get; set; }

        public int? esubs_parnter_id { get; set; }

        public int? SAPAccountNo { get; set; }

        public decimal? profit_raised { get; set; }

        public virtual ICollection<fc_testimonial> fc_testimonial { get; set; }
    }
}
