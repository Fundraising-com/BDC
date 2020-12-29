namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertising_Support
    {
        public Advertising_Support()
        {
            Advertising_Support_Contact = new HashSet<Advertising_Support_Contact>();
            Targeted_Market = new HashSet<Targeted_Market>();
            Competitor_Advertising = new HashSet<Competitor_Advertising>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Advertising_Support_ID { get; set; }

        public int Advertising_Support_Type_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Publishnig_Date { get; set; }

        [StringLength(100)]
        public string Web_Site { get; set; }

        [StringLength(25)]
        public string Ordering_Phone_Number { get; set; }

        public int? Periodicity { get; set; }

        public int? Nb_Draw { get; set; }

        public decimal? Magazine_Price { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        public virtual Advertising_Support_Type Advertising_Support_Type { get; set; }

        public virtual ICollection<Advertising_Support_Contact> Advertising_Support_Contact { get; set; }

        public virtual ICollection<Targeted_Market> Targeted_Market { get; set; }

        public virtual ICollection<Competitor_Advertising> Competitor_Advertising { get; set; }
    }
}
