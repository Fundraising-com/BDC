namespace GA.BDC.Data.Fundraising.FastFundraising.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("category")]
    public partial class category
    {
        [StringLength(50)]
        public string categoryname { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int categoryid { get; set; }

        public int? catdisplayitemid { get; set; }

        [StringLength(50)]
        public string displayitemimagepath { get; set; }

        public int? status { get; set; }

        public int? SortOrder { get; set; }

        public int? shipping_group_id { get; set; }
    }
}
