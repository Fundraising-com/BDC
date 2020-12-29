namespace GA.BDC.Data.Fundraising.EFRCommon.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class advertising_type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int advertising_type_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public DateTime create_date { get; set; }
    }
}
