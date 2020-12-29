namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Template")]
    public partial class Template
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Partner_ID { get; set; }

        [StringLength(256)]
        public string Template_Path { get; set; }

        [StringLength(50)]
        public string ReportCenterPasswd { get; set; }
    }
}
