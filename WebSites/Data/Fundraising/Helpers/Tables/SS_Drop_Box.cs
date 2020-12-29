namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SS_Drop_Box
    {
        public SS_Drop_Box()
        {
            SS_Drop_Box_Package = new HashSet<SS_Drop_Box_Package>();
        }

        [Key]
        public int SS_Drop_Box_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SS_Drop_Box_Name { get; set; }

        public int? Display_Order { get; set; }

        public virtual ICollection<SS_Drop_Box_Package> SS_Drop_Box_Package { get; set; }
    }
}
