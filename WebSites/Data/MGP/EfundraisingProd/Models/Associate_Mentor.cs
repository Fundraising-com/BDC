namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Associate_Mentor
    {
        public Associate_Mentor()
        {
            Associate_Mentor_Comment = new HashSet<Associate_Mentor_Comment>();
            associate_mentor_commission = new HashSet<associate_mentor_commission>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Associate_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Mentor_ID { get; set; }

        public DateTime? Start_Date { get; set; }

        public DateTime? End_Date { get; set; }

        public virtual consultant consultant { get; set; }

        public virtual consultant consultant1 { get; set; }

        public virtual ICollection<Associate_Mentor_Comment> Associate_Mentor_Comment { get; set; }

        public virtual ICollection<associate_mentor_commission> associate_mentor_commission { get; set; }
    }
}
