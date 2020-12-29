namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Supporter_Email_Sent
    {
        [Key]
        public int Supporter_Email_Sent_ID { get; set; }

        public int Email_Type_ID { get; set; }

        public int Supporter_ID { get; set; }

        public DateTime? Date_Sent { get; set; }
    }
}
