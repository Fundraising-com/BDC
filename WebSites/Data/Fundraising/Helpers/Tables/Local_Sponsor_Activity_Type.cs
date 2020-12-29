namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Local_Sponsor_Activity_Type
    {
        public Local_Sponsor_Activity_Type()
        {
            Local_Sponsor_Activity = new HashSet<Local_Sponsor_Activity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Local_Sponsor_Activity_Type_Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Local_Sponsor_Activity> Local_Sponsor_Activity { get; set; }
    }
}
