namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("salutation")]
    public partial class salutation
    {
        public salutation()
        {
            salutation_desc1 = new HashSet<salutation_desc>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte salutation_id { get; set; }

        [Required]
        [StringLength(15)]
        public string salutation_desc { get; set; }

        public virtual ICollection<salutation_desc> salutation_desc1 { get; set; }
    }
}
