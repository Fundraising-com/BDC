namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Collection_Status
    {
        public Collection_Status()
        {
            payments = new HashSet<payment>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Collection_Status_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<payment> payments { get; set; }
    }
}
