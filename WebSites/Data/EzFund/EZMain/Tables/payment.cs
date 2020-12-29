using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("payment")]
    public partial class payment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sales_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int payment_no { get; set; }

        [StringLength(16)]
        public string payment_method { get; set; }

        public int? collection_status_id { get; set; }

        public DateTime payment_entry_date { get; set; }

        public DateTime cashable_date { get; set; }

        [StringLength(16)]
        public string credit_card_no { get; set; }

        [StringLength(7)]
        public string expiry_date { get; set; }

        [StringLength(50)]
        public string name_on_card { get; set; }

        [StringLength(10)]
        public string authorization_number { get; set; }

        public decimal payment_amount { get; set; }

        public bool commission_paid { get; set; }

        public int? foreign_orderid { get; set; }

        public DateTime create_date { get; set; }

        public int? ext_payment_id { get; set; }

        public int? create_user_id { get; set; }

        public int? payment_status_id { get; set; }

        public virtual Collection_Status Collection_Status { get; set; }
    }
}
