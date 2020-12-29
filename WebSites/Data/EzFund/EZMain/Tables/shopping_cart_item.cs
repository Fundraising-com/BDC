using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    
    [Table("shopping_cart_item")]
    public partial class shopping_cart_item
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int id { get; set; }

        public int? parent_id { get; set; }

        [Required]
        public int shopping_cart_id { get; set; }

        public int? product_id { get; set; }

        public string item_code { get; set; }

        [Required]
        public int quantity { get; set; }

        [StringLength(400)]
        public string comments{ get; set; }

        [Required]
        public DateTime created { get; set; }
    }
}
