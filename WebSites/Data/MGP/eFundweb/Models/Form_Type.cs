namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Form_Type
    {
        public Form_Type()
        {
            Partners_Forms = new HashSet<Partners_Forms>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Form_Type_ID { get; set; }

        [StringLength(200)]
        public string Form_Type_Name { get; set; }

        public int Lead_Status_ID { get; set; }

        public virtual ICollection<Partners_Forms> Partners_Forms { get; set; }
    }
}
