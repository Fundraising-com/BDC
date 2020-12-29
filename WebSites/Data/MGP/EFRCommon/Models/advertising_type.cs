namespace GA.BDC.Data.MGP.EFRCommon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class advertising_type
    {
        public advertising_type()
        {
            advertisings = new HashSet<advertising>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int advertising_type_id { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public DateTime create_date { get; set; }

        public virtual ICollection<advertising> advertisings { get; set; }
    }
}
