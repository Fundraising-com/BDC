namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Web_Site
    {
        public Web_Site()
        {
            Entry_Form = new HashSet<Entry_Form>();
        }

        [Key]
        public int Web_Site_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Web_Site_Name { get; set; }

        public virtual ICollection<Entry_Form> Entry_Form { get; set; }
    }
}
