namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
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
            Question_Entry_Form = new HashSet<Question_Entry_Form>();
        }

        [Key]
        public int Entry_Form_ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Entry_Form_Desc { get; set; }

        [StringLength(255)]
        public string Master_Template { get; set; }

        [StringLength(255)]
        public string Content_Template { get; set; }

        public int? Web_Site_ID { get; set; }

        public virtual Web_Site Web_Site { get; set; }

        public virtual ICollection<Question_Entry_Form> Question_Entry_Form { get; set; }
    }
}
