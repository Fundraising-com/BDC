namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class fr_pendingleads
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string custname { get; set; }

        [StringLength(255)]
        public string cntcprsn { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string state { get; set; }

        public double? zip { get; set; }

        [StringLength(255)]
        public string phone1 { get; set; }

        [StringLength(255)]
        public string emailaddress1 { get; set; }

        [StringLength(255)]
        public string besttimetocall { get; set; }

        [StringLength(255)]
        public string GroupType { get; set; }

        [StringLength(255)]
        public string TargetProfit { get; set; }

        public double? NumberOfSellers { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Requestdate { get; set; }

        [StringLength(255)]
        public string SampleRequestDetails { get; set; }

        [StringLength(255)]
        public string SourceCode { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? createdate { get; set; }

        [StringLength(255)]
        public string processeddate { get; set; }

        [StringLength(255)]
        public string processedby { get; set; }

        public double? rowid { get; set; }

        [StringLength(255)]
        public string adoptedby { get; set; }

        [StringLength(255)]
        public string adoptedDate { get; set; }

        [StringLength(255)]
        public string category { get; set; }

        public double? salesid { get; set; }

        [StringLength(255)]
        public string dnotes { get; set; }

        [StringLength(255)]
        public string callbackdatetime { get; set; }

        [StringLength(255)]
        public string salescategory { get; set; }

        [StringLength(255)]
        public string hearaboutus { get; set; }

        public double? callyesno { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool done { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool remove { get; set; }
    }
}
