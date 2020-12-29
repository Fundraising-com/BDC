namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AR_Activity_Type
    {
        public AR_Activity_Type()
        {
            AR_Activity = new HashSet<AR_Activity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AR_Activity_Type_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<AR_Activity> AR_Activity { get; set; }
    }
}
