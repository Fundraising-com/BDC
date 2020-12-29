namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class lead_activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int lead_activity_id { get; set; }

        public int lead_id { get; set; }

        public int lead_activity_type_id { get; set; }

        public DateTime lead_activity_date { get; set; }

        public DateTime? completed_date { get; set; }

        [Column(TypeName = "text")]
        public string comments { get; set; }

        public virtual lead lead { get; set; }

        public virtual Lead_Activity_Type Lead_Activity_Type { get; set; }
    }
}
