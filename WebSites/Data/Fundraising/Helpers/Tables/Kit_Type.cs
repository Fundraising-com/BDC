namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Kit_Type
    {
        public Kit_Type()
        {
            promotional_kit = new HashSet<promotional_kit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Kit_Type_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public DateTime? Delivery_Time { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public bool? Is_Default { get; set; }

        public bool? is_active { get; set; }

        public DateTime? create_date { get; set; }

        public virtual ICollection<promotional_kit> promotional_kit { get; set; }
    }
}
