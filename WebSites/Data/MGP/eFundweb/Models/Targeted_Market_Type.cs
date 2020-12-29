namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Targeted_Market_Type
    {
        public Targeted_Market_Type()
        {
            Targeted_Market = new HashSet<Targeted_Market>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Targeted_Market_Type_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public bool Decision_Maker { get; set; }

        public int? Group_Type_ID { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual Group_Type Group_Type { get; set; }

        public virtual ICollection<Targeted_Market> Targeted_Market { get; set; }
    }
}
