namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_Status
    {
        public Sales_Status()
        {
            sale_to_add = new HashSet<sale_to_add>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_Status_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }
    }
}
