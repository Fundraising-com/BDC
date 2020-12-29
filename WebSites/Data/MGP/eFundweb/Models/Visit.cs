namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visit")]
    public partial class Visit
    {
        [Key]
        public int Visit_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Session_ID { get; set; }

        [StringLength(255)]
        public string Ads_Script { get; set; }

        [StringLength(255)]
        public string Referer { get; set; }

        public int Expire { get; set; }

        public bool? Filled_Form { get; set; }

        public int? Promotion_ID { get; set; }

        [StringLength(255)]
        public string Cookie_Content { get; set; }

        public int? Temp_Lead_ID { get; set; }

        public DateTime? Entry_Date { get; set; }

        [StringLength(255)]
        public string Host { get; set; }

        [StringLength(255)]
        public string Query_String { get; set; }

        [StringLength(255)]
        public string Script_Name { get; set; }

        [StringLength(50)]
        public string GTSE { get; set; }

        [StringLength(100)]
        public string Keyword { get; set; }

        [StringLength(100)]
        public string PID { get; set; }

        public int? Web_Site_ID { get; set; }

        public int? Partner_ID { get; set; }

        public bool Has_Visited_Form_Page { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Elapsed_Completion_Time { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Elapsed_Visit_Time { get; set; }

        public bool Is_Submitted { get; set; }

        public short? Viewed_Page { get; set; }

        [StringLength(15)]
        public string IP_Address { get; set; }
    }
}
