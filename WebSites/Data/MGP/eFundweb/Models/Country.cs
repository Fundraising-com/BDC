namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            C_tbd_partner = new HashSet<C_tbd_partner>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Country_ID { get; set; }

        [Required]
        [StringLength(80)]
        public string Country_Name { get; set; }

        [Required]
        [StringLength(5)]
        public string Currency_Code { get; set; }

        [StringLength(5)]
        public string Country_Code { get; set; }

        public virtual ICollection<C_tbd_partner> C_tbd_partner { get; set; }
    }
}
