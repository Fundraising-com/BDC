namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AR_Status
    {
        public AR_Status()
        {
            Commission_Paid = new HashSet<Commission_Paid>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AR_Status_ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public bool? Commission_On_Hold { get; set; }

        public bool? Commission_Is_Payable { get; set; }

        public bool? Commission_Is_Credited { get; set; }

        public virtual ICollection<Commission_Paid> Commission_Paid { get; set; }
    }
}
