namespace GA.BDC.Data.MGP.EFREcommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class credit_card
    {
        public credit_card()
        {
            credit_card_authorization = new HashSet<credit_card_authorization>();
            orders = new HashSet<order>();
        }

        [Key]
        public int credit_card_id { get; set; }

        [StringLength(100)]
        public string credit_card_number { get; set; }

        [StringLength(100)]
        public string credit_card_name { get; set; }

        public int? credit_card_cvv2 { get; set; }

        public int credit_card_type_id { get; set; }

        public int? credit_card_expiration_month { get; set; }

        public int? credit_card_expiration_year { get; set; }

        public bool deleted { get; set; }

        public DateTime create_date { get; set; }

        public int create_user_id { get; set; }

        public DateTime? update_date { get; set; }

        public int? update_user_id { get; set; }

        public int? phone_number_id { get; set; }

        public int? postal_address_id { get; set; }

        [StringLength(20)]
        public string last_cc_digits { get; set; }

        [StringLength(50)]
        public string GUID { get; set; }

        public virtual ICollection<credit_card_authorization> credit_card_authorization { get; set; }

        public virtual phone_number phone_number { get; set; }

        public virtual postal_address postal_address { get; set; }

        public virtual credit_card_type credit_card_type { get; set; }

        public virtual ICollection<order> orders { get; set; }
    }
}
