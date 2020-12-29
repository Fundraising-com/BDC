namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class best_time_call
    {
        public best_time_call()
        {
            best_time_call_desc1 = new HashSet<best_time_call_desc>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte best_time_call_id { get; set; }

        [Required]
        [StringLength(25)]
        public string best_time_call_desc { get; set; }

        public virtual ICollection<best_time_call_desc> best_time_call_desc1 { get; set; }
    }
}
