namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lead_Duplicates_Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LEAD_DUPLICATES_LOGIN_ID { get; set; }

        [StringLength(255)]
        public string DUPLICATES_FOUND { get; set; }

        [StringLength(255)]
        public string RELATED_TABLE { get; set; }

        [StringLength(255)]
        public string DETECTED_FIELDS { get; set; }

        [StringLength(255)]
        public string FIELDS_VALUES { get; set; }

        [StringLength(255)]
        public string NT_LOGIN { get; set; }

        [StringLength(255)]
        public string MACHINE { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? TIME_STAMP { get; set; }
    }
}
