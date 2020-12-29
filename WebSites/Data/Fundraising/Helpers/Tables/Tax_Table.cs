namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tax_Table
    {
        public Tax_Table()
        {
            Applicable_Tax = new HashSet<Applicable_Tax>();
            Applicable_Tax_To_Add = new HashSet<Applicable_Tax_To_Add>();
            State_Tax = new HashSet<State_Tax>();
        }

        [Key]
        [StringLength(4)]
        public string Tax_Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(20)]
        public string Tax_Account_Number { get; set; }

        [StringLength(50)]
        public string Description_francaise { get; set; }

        public virtual ICollection<Applicable_Tax> Applicable_Tax { get; set; }

        public virtual ICollection<Applicable_Tax_To_Add> Applicable_Tax_To_Add { get; set; }

        public virtual ICollection<State_Tax> State_Tax { get; set; }
    }
}
