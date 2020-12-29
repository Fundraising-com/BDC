namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class consultant_transfer_status
    {
        public consultant_transfer_status()
        {
            consultants = new HashSet<consultant>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte consultant_transfer_status_id { get; set; }

        [Required]
        [StringLength(25)]
        public string consultant_transfer_status_desc { get; set; }

        public virtual ICollection<consultant> consultants { get; set; }
    }
}
