namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog")]
    public partial class catalog
    {
        public catalog()
        {
            products = new HashSet<product>();
        }

        [Key]
        public int catalog_id { get; set; }

        [Required]
        [StringLength(50)]
        public string catalog_name { get; set; }

        public int? status_id { get; set; }

        [Required]
        [StringLength(5)]
        public string culture_code { get; set; }

        public DateTime create_date { get; set; }

        public virtual status status { get; set; }

        public virtual ICollection<product> products { get; set; }
    }
}
