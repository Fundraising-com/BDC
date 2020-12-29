namespace GA.BDC.Data.ESubs.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Trace_leads
    {
        [Key]
        public int RowNumber { get; set; }

        public int? EventClass { get; set; }

        [Column(TypeName = "ntext")]
        public string TextData { get; set; }

        public int? DatabaseID { get; set; }

        [StringLength(128)]
        public string NTUserName { get; set; }

        [StringLength(128)]
        public string NTDomainName { get; set; }

        public int? ClientProcessID { get; set; }

        [StringLength(128)]
        public string ApplicationName { get; set; }

        [StringLength(128)]
        public string LoginName { get; set; }

        public int? SPID { get; set; }

        public long? Duration { get; set; }

        public DateTime? StartTime { get; set; }

        public long? Reads { get; set; }

        public long? Writes { get; set; }

        public int? CPU { get; set; }

        public int? ObjectID { get; set; }

        public int? Success { get; set; }

        [StringLength(128)]
        public string ServerName { get; set; }

        public int? ObjectType { get; set; }

        public int? State { get; set; }

        [StringLength(128)]
        public string ObjectName { get; set; }

        [StringLength(128)]
        public string DatabaseName { get; set; }

        [StringLength(128)]
        public string DBUserName { get; set; }
    }
}
