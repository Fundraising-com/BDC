namespace GA.BDC.Data.Fundraising.EFundStore.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("question")]
    public partial class question
    {
        [Key]
        public int question_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(600)]
        public string description { get; set; }

        public int? control_type_id { get; set; }

        [StringLength(100)]
        public string field_name { get; set; }

        [StringLength(100)]
        public string default_value { get; set; }

        public int? min_lenght { get; set; }

        public int? max_lenght { get; set; }

        public int? nbr_value { get; set; }

        public DateTime? datestamp { get; set; }

        [StringLength(200)]
        public string stored_proc_to_call { get; set; }

        [StringLength(20)]
        public string answer_type { get; set; }

        [StringLength(100)]
        public string field_value { get; set; }
    }
}
