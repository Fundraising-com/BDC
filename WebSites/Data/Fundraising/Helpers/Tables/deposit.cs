namespace GA.BDC.Data.Fundraising.EFundraisingProd.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("deposit")]
    public partial class deposit
    {
        public deposit()
        {
            Deposit_Item = new HashSet<Deposit_Item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int deposit_id { get; set; }

        public byte payment_method_id { get; set; }

        public int bank_id { get; set; }

        [Required]
        [StringLength(50)]
        public string bank_account_no { get; set; }

        public DateTime deposit_date { get; set; }

        public virtual Bank_Account Bank_Account { get; set; }

        public virtual ICollection<Deposit_Item> Deposit_Item { get; set; }
    }
}
