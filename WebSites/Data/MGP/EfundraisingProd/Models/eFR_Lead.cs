namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class eFR_Lead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Lead_ID { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(100)]
        public string Organization_Name { get; set; }

        [StringLength(60)]
        public string Promotion_Description { get; set; }

        [StringLength(255)]
        public string Lead_Activity_Detail { get; set; }

        [StringLength(255)]
        public string Lead_Comment { get; set; }

        public DateTime? Activity_Scheduled_Date { get; set; }

        public int? Consultant_ID { get; set; }

        public int? Consultant_Ext { get; set; }

        public int? Is_Done { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone_Number { get; set; }

        [StringLength(15)]
        public string phone_extension { get; set; }

        [StringLength(50)]
        public string Promotion_Type { get; set; }

        [Column("2ndPhone_Number")]
        [StringLength(20)]
        public string C2ndPhone_Number { get; set; }

        [Column("2ndPhone_Extension")]
        [StringLength(15)]
        public string C2ndPhone_Extension { get; set; }
    }
}
