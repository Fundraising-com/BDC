namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Competitor_Advertising
    {
        public Competitor_Advertising()
        {
            Advertising_Support = new HashSet<Advertising_Support>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Competitor_Advertising_ID { get; set; }

        public int Competitor_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(25)]
        public string Publicity_Duration { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual Competitor Competitor { get; set; }

        public virtual ICollection<Advertising_Support> Advertising_Support { get; set; }
    }
}
