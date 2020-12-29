namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Entry_Form
    {
        public Entry_Form()
        {
            Partners_Forms = new HashSet<Partners_Forms>();
            Questions_Entry_Form = new HashSet<Questions_Entry_Form>();
            Web_Visit = new HashSet<Web_Visit>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Entry_Form_ID { get; set; }

        [Required]
        [StringLength(600)]
        public string Entry_Form_Desc { get; set; }

        public virtual ICollection<Partners_Forms> Partners_Forms { get; set; }

        public virtual ICollection<Questions_Entry_Form> Questions_Entry_Form { get; set; }

        public virtual ICollection<Web_Visit> Web_Visit { get; set; }
    }
}
