namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class mag_lead
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(8000)]
        public string BATCHNO { get; set; }

        [StringLength(8000)]
        public string SEQNO { get; set; }

        [StringLength(8000)]
        public string ISS_MM { get; set; }

        [StringLength(8000)]
        public string ISS_DD { get; set; }

        [StringLength(8000)]
        public string ISS_YY { get; set; }

        [StringLength(8000)]
        public string PUB_CODE { get; set; }

        [StringLength(8000)]
        public string CARD_TYPE { get; set; }

        [StringLength(8000)]
        public string FIRST { get; set; }

        [StringLength(8000)]
        public string MIDDLE { get; set; }

        [StringLength(8000)]
        public string LAST { get; set; }

        [StringLength(8000)]
        public string COMPANY { get; set; }

        [StringLength(8000)]
        public string ADDRESS1 { get; set; }

        [StringLength(8000)]
        public string ADDRESS2 { get; set; }

        [StringLength(8000)]
        public string CITY { get; set; }

        [StringLength(8000)]
        public string STATE { get; set; }

        [StringLength(8000)]
        public string ZIP { get; set; }

        [StringLength(8000)]
        public string PLUS4 { get; set; }

        [StringLength(8000)]
        public string PHONE { get; set; }

        [StringLength(8000)]
        public string COUNTRY { get; set; }

        [StringLength(8000)]
        public string RS_NO { get; set; }

        [StringLength(8000)]
        public string EMAIL { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }

        public int? remove { get; set; }
    }
}
