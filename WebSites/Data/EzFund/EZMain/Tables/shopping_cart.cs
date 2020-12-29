using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("shopping_cart")]
    public partial class shopping_cart
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int id { get; set; }

        [StringLength(128)]
        public string user_id { get; set; }

        [Required]
        public int status { get; set; }

        [StringLength(400)]
        public string comments { get; set; }

        [Required]
        public DateTime created { get; set; }

        public int? order_id { get; set; }

        public int? promotion_code_id { get; set; }
    }
}
