namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Web_Visit_ID { get; set; }

        public int? Promotion_ID { get; set; }

        public int? Entry_Form_ID { get; set; }

        public int? Temp_Lead_ID { get; set; }

        public DateTime? Entry_Date { get; set; }

        [StringLength(255)]
        public string Referrer { get; set; }

        [StringLength(100)]
        public string URL { get; set; }

        [StringLength(255)]
        public string Query_String { get; set; }

        [StringLength(255)]
        public string Host { get; set; }

        public virtual Entry_Form Entry_Form { get; set; }

        public virtual Temp_Lead Temp_Lead { get; set; }
    }
}
