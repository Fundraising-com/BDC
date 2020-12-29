namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class General_Comment
    {
        [Key]
        public int General_Comment_Id { get; set; }

        public int? Lead_Id { get; set; }

        public int? Sales_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Entry_Date { get; set; }

        [Column("General_Comment", TypeName = "text")]
        public string General_Comment1 { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Name { get; set; }

        public int Department_ID { get; set; }

        public virtual Department Department { get; set; }

        public virtual lead lead { get; set; }
    }
}
