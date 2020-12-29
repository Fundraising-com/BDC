namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Questions_Entry_Form
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Questions_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Entry_Form_ID { get; set; }

        public bool? Is_Required { get; set; }

        public int? Questions_Order { get; set; }

        [StringLength(100)]
        public string Insert_Table { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string Insert_Column { get; set; }

        [StringLength(200)]
        public string Default_Value { get; set; }

        public byte Instance_Visibility { get; set; }

        public virtual Entry_Form Entry_Form { get; set; }

        public virtual Question Question { get; set; }
    }
}
