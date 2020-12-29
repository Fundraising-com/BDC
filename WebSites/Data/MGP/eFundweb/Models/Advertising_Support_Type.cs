namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertising_Support_Type
    {
        public Advertising_Support_Type()
        {
            Advertising_Support = new HashSet<Advertising_Support>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertising_Support_Type_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual ICollection<Advertising_Support> Advertising_Support { get; set; }
    }
}
