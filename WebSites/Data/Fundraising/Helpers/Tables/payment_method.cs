namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class payment_method
    {
        public payment_method()
        {
            credit_card_types = new HashSet<credit_card_types>();
            payments = new HashSet<payment>();
            sale_to_add = new HashSet<sale_to_add>();
            sale_to_add1 = new HashSet<sale_to_add>();
        }

        [Key]
        public byte payment_method_id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public bool is_negative { get; set; }

        public decimal discount_percentage { get; set; }

        public int? ext_payment_type_id { get; set; }

        public virtual ICollection<credit_card_types> credit_card_types { get; set; }

        public virtual ICollection<payment> payments { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add { get; set; }

        public virtual ICollection<sale_to_add> sale_to_add1 { get; set; }
    }
}
