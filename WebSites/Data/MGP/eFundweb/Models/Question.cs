namespace GA.BDC.Data.MGP.eFundweb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {
        public Question()
        {
            Questions_Desc = new HashSet<Questions_Desc>();
            Questions_Entry_Form = new HashSet<Questions_Entry_Form>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Questions_ID { get; set; }

        [StringLength(100)]
        public string Questions_Name { get; set; }

        [StringLength(600)]
        public string Questions_Description { get; set; }

        [StringLength(100)]
        public string Questions_Type { get; set; }

        [StringLength(100)]
        public string Validation_Type { get; set; }

        public int? Min_Lenght { get; set; }

        public int? Max_Lenght { get; set; }

        public int Nbr_Values { get; set; }

        public virtual ICollection<Questions_Desc> Questions_Desc { get; set; }

        public virtual ICollection<Questions_Entry_Form> Questions_Entry_Form { get; set; }
    }
}
