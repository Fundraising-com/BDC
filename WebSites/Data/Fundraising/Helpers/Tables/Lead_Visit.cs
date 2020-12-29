namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Visit
    {
        public Lead_Visit()
        {
            promotional_kit = new HashSet<promotional_kit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_Visit_ID { get; set; }

        public int? Promotion_ID { get; set; }

        public int? Lead_ID { get; set; }

        public int? Temp_Lead_ID { get; set; }

        public DateTime? Visit_Date { get; set; }

        [StringLength(4)]
        public string Channel_Code { get; set; }

        public virtual lead lead { get; set; }

        public virtual Lead_Channel Lead_Channel { get; set; }

        public virtual ICollection<promotional_kit> promotional_kit { get; set; }
    }
}
