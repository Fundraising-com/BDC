namespace GA.BDC.Data.MGP.fastfundraising.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ffitem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemid { get; set; }

        public int? categoryid { get; set; }

        [StringLength(100)]
        public string itemname { get; set; }

        [StringLength(50)]
        public string itemnmbr { get; set; }

        [StringLength(100)]
        public string imagepath { get; set; }

        [StringLength(100)]
        public string descriptionpath { get; set; }

        public int? status { get; set; }

        [StringLength(50)]
        public string itemuom { get; set; }

        [StringLength(100)]
        public string peruom { get; set; }

        public double? enduserprice { get; set; }

        public double? baseprice { get; set; }

        public double? strikeprice { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [Column(TypeName = "text")]
        public string flavors { get; set; }

        [Column(TypeName = "text")]
        public string packaging { get; set; }

        public int minimumqty { get; set; }

        public int? SortOrder { get; set; }
    }
}
