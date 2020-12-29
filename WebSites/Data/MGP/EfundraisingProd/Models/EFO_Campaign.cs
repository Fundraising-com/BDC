namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFO_Campaign
    {
        public EFO_Campaign()
        {
            EFO_Campaign_Status = new HashSet<EFO_Campaign_Status>();
            EFO_Participant = new HashSet<EFO_Participant>();
        }

        [Key]
        public int Campaign_ID { get; set; }

        public int Group_Type_ID { get; set; }

        public int QSP_Program_ID { get; set; }

        public int Campaign_Image_ID { get; set; }

        public int Organizer_ID { get; set; }

        [StringLength(50)]
        public string Group_Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Creation_Date { get; set; }

        public decimal Financial_Goal { get; set; }

        [Required]
        [StringLength(200)]
        public string Fund_Raising_Reason { get; set; }

        [StringLength(200)]
        public string Background_Info { get; set; }

        [StringLength(150)]
        public string Comments { get; set; }

        public bool Is_Launched { get; set; }

        public bool Is_Over { get; set; }

        [StringLength(15)]
        public string Account_Number { get; set; }

        public virtual ICollection<EFO_Campaign_Status> EFO_Campaign_Status { get; set; }

        public virtual ICollection<EFO_Participant> EFO_Participant { get; set; }
    }
}
