namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Business_Rule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Business_Rule_ID { get; set; }

        public int Partner_ID { get; set; }

        [StringLength(100)]
        public string Rule_Description { get; set; }

        [StringLength(25)]
        public string Module_Name { get; set; }

        [StringLength(50)]
        public string Form_Name { get; set; }

        [StringLength(50)]
        public string Access_Sub_Name { get; set; }
    }
}
