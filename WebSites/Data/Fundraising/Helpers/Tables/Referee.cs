namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Referee")]
    public partial class Referee
    {
        public Referee()
        {
            leads = new HashSet<lead>();
        }

        [Key]
        public int Referee_Id { get; set; }

        public int Lead_Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Entry_Date { get; set; }

        [Required]
        [StringLength(25)]
        public string First_Name { get; set; }

        [Required]
        [StringLength(25)]
        public string Last_Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Phone_Number { get; set; }

        public bool Is_Entered { get; set; }

        public virtual ICollection<lead> leads { get; set; }

        public virtual lead lead { get; set; }
    }
}
