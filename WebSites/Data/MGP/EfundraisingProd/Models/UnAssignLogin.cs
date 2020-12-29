namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnAssignLogin")]
    public partial class UnAssignLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UnAssignLogin_Id { get; set; }

        [StringLength(50)]
        public string User_Name { get; set; }

        public int? Consultant_Id { get; set; }

        public int? Lead_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Unassignment_TimeStamp { get; set; }
    }
}
