namespace GA.BDC.Data.MGP.EfundraisingProd.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class perfom_trace
    {
        [Key]
        public int RowNumber { get; set; }

        public int? EventClass { get; set; }

        [Column(TypeName = "ntext")]
        public string TextData { get; set; }

        [StringLength(128)]
        public string NTUserName { get; set; }

        [StringLength(128)]
        public string HostName { get; set; }

        [StringLength(128)]
        public string ApplicationName { get; set; }

        [StringLength(128)]
        public string LoginName { get; set; }

        public int? SPID { get; set; }

        public long? Duration { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? CPU { get; set; }

        public int? EventSubClass { get; set; }

        public int? IntegerData { get; set; }

        public int? Error { get; set; }

        [StringLength(128)]
        public string ObjectName { get; set; }

        [StringLength(128)]
        public string DatabaseName { get; set; }
    }
}
