namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mailing_Code
    {
        [Key]
        public int Mailing_Code_ID { get; set; }

        [Required]
        [StringLength(25)]
        public string List_Name { get; set; }

        public int List_ID { get; set; }

        [Required]
        [StringLength(25)]
        public string Flyer_Code { get; set; }

        public DateTime? Launch_Date { get; set; }

        [Required]
        [StringLength(25)]
        public string Mailing_Code_Label { get; set; }

        public int? Mailing_Name_ID { get; set; }
    }
}
