namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sponsor_Found_Stool
    {
        [Key]
        [Column(Order = 0)]
        public int Stool_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sales_ID { get; set; }

        [StringLength(25)]
        public string User_Name { get; set; }

        public bool? Valeur { get; set; }

        [StringLength(50)]
        public string Modif_Date { get; set; }
    }
}
