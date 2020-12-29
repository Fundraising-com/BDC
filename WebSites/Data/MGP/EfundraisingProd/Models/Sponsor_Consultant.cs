namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sponsor_Consultant
    {
        public Sponsor_Consultant()
        {
            Local_Sponsor = new HashSet<Local_Sponsor>();
            Local_Sponsor_Activity = new HashSet<Local_Sponsor_Activity>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sponsor_Consultant_ID { get; set; }

        [StringLength(50)]
        public string First_Name { get; set; }

        [Column("Middle Initial")]
        [StringLength(50)]
        public string Middle_Initial { get; set; }

        [StringLength(50)]
        public string Last_Name { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(20)]
        public string Day_Phone { get; set; }

        [StringLength(20)]
        public string Day_Time_Call { get; set; }

        [StringLength(20)]
        public string Evening_Phone { get; set; }

        [StringLength(20)]
        public string Evenig_Time_Call { get; set; }

        [StringLength(50)]
        public string Alternate_Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public bool Is_Active { get; set; }

        [StringLength(50)]
        public string Nt_Login { get; set; }

        public double? Commission_Rate { get; set; }

        public virtual ICollection<Local_Sponsor> Local_Sponsor { get; set; }

        public virtual ICollection<Local_Sponsor_Activity> Local_Sponsor_Activity { get; set; }
    }
}
