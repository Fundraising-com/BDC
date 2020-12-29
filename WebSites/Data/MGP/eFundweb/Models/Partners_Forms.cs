namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partners_Forms
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Partner_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Entry_Form_ID { get; set; }

        [StringLength(600)]
        public string Recipients { get; set; }

        public int? Form_Type_ID { get; set; }

        public virtual Entry_Form Entry_Form { get; set; }

        public virtual Form_Type Form_Type { get; set; }
    }
}
