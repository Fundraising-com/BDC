namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Campaign_Reason
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Campaign_Reason_ID { get; set; }

        public int Party_Type_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Campaign_Reason_Desc { get; set; }

        [StringLength(50)]
        public string Campaign_Reason_Desc_Fr { get; set; }
    }
}
