namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ARDHISP")]
    public partial class ARDHISP
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string ARGORF { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal ARCUST { get; set; }

        [Key]
        [Column("AR#SEQ", Order = 2)]
        public decimal AR_SEQ { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal ARCNTR { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal ARYRTR { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal ARMOTR { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal ARDYTR { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal ARAMTR { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(2)]
        public string ARCDTR { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(6)]
        public string ARORDR { get; set; }

        [Key]
        [Column("AR#ORD", Order = 10)]
        public decimal AR_ORD { get; set; }

        [Key]
        [Column("AR#IFM", Order = 11)]
        public decimal AR_IFM { get; set; }

        [Key]
        [Column(Order = 12)]
        public decimal ARAMBL { get; set; }

        [Key]
        [Column(Order = 13)]
        public decimal ARAMCR { get; set; }

        [Key]
        [Column(Order = 14)]
        public decimal ARAMCL { get; set; }

        [Key]
        [Column(Order = 15)]
        public decimal ARAMNC { get; set; }

        [Key]
        [Column(Order = 16)]
        [StringLength(1)]
        public string ARCDNC { get; set; }

        [Key]
        [Column("AR#CHK", Order = 17)]
        public decimal AR_CHK { get; set; }

        [Key]
        [Column(Order = 18)]
        [StringLength(1)]
        public string ARAUTO { get; set; }

        [Key]
        [Column(Order = 19)]
        public decimal ARCNIV { get; set; }

        [Key]
        [Column(Order = 20)]
        public decimal ARYRIV { get; set; }

        [Key]
        [Column(Order = 21)]
        public decimal ARMOIV { get; set; }

        [Key]
        [Column(Order = 22)]
        public decimal ARDYIV { get; set; }

        [Key]
        [Column("AR#ISQ", Order = 23)]
        public decimal AR_ISQ { get; set; }

        [Key]
        [Column("AR#BAT", Order = 24)]
        public decimal AR_BAT { get; set; }

        [Key]
        [Column("AR#BSQ", Order = 25)]
        public decimal AR_BSQ { get; set; }

        [Key]
        [Column(Order = 26)]
        [StringLength(1)]
        public string ARCDCL { get; set; }

        [Key]
        [Column(Order = 27)]
        [StringLength(1)]
        public string ARNSET { get; set; }

        [Key]
        [Column(Order = 28)]
        [StringLength(1)]
        public string ARSOBA { get; set; }

        [Key]
        [Column("AR#SSQ", Order = 29)]
        public decimal AR_SSQ { get; set; }

        [Key]
        [Column(Order = 30)]
        [StringLength(1)]
        public string ARRFLG { get; set; }

        [Key]
        [Column(Order = 31)]
        [StringLength(1)]
        public string ARCDFC { get; set; }
    }
}
