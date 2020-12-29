namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Competitor")]
    public partial class Competitor
    {
        public Competitor()
        {
            Competitor_Advertising = new HashSet<Competitor_Advertising>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Competitor_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Business_Name { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual ICollection<Competitor_Advertising> Competitor_Advertising { get; set; }
    }
}
