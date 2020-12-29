namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Organizer
    {
        [Key]
        public int Organizer_ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string User_Name { get; set; }

        [StringLength(15)]
        public string Password { get; set; }

        [StringLength(15)]
        public string Title { get; set; }

        [StringLength(75)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Best_Time_To_Call { get; set; }

        [StringLength(15)]
        public string Evening_Phone { get; set; }

        [StringLength(15)]
        public string Day_Phone { get; set; }

        [StringLength(15)]
        public string Fax_Number { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Entry_Date { get; set; }

        [StringLength(150)]
        public string Comments { get; set; }

        public int Organization_ID { get; set; }

        public int? School_ID { get; set; }
    }
}
