namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Comments_ID { get; set; }

        public int? Priority_ID { get; set; }

        public int? Sales_ID { get; set; }

        public int? Consultant_ID { get; set; }

        public int? Lead_ID { get; set; }

        public int? Department_ID { get; set; }

        public DateTime? Entry_Date { get; set; }

        [Column(TypeName = "text")]
        public string Comments { get; set; }

        public virtual Department Department { get; set; }

        public virtual lead lead { get; set; }

        public virtual Priority Priority { get; set; }
    }
}
