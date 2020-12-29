namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inventory_Adjustment_Type
    {
        public Inventory_Adjustment_Type()
        {
            Inventory_Adjustment = new HashSet<Inventory_Adjustment>();
        }

        [Key]
        public int Inventory_Adjustment_Type_ID { get; set; }

        [StringLength(255)]
        public string Inventory_Adjustment_Type_Desc { get; set; }

        public virtual ICollection<Inventory_Adjustment> Inventory_Adjustment { get; set; }
    }
}
