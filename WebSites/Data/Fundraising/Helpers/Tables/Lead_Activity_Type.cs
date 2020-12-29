namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Activity_Type
    {
        public Lead_Activity_Type()
        {
            Efr_Lead_Activity = new HashSet<Efr_Lead_Activity>();
            lead_activity = new HashSet<lead_activity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Activity_Type_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int? Priority { get; set; }

        public virtual ICollection<Efr_Lead_Activity> Efr_Lead_Activity { get; set; }

        public virtual ICollection<lead_activity> lead_activity { get; set; }
    }
}
