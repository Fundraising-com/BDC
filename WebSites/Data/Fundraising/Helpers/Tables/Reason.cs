namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reason")]
    public partial class Reason
    {
        public Reason()
        {
            Adjustments = new HashSet<Adjustment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Reason_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public bool Is_Active { get; set; }

        public int? ext_adjustment_type_id { get; set; }

        public virtual ICollection<Adjustment> Adjustments { get; set; }
    }
}
