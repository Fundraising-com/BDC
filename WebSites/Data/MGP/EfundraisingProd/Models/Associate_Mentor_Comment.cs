namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Associate_Mentor_Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ass_Mentor_Comment_ID { get; set; }

        public int Associate_ID { get; set; }

        public int Mentor_ID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Comment_Date { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual Associate_Mentor Associate_Mentor { get; set; }
    }
}
