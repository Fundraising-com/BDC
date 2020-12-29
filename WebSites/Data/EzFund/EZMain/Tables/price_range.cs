using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("price_range")]
    public partial class price_range
	{
        [Key]
        public int price_range_id { get; set; }

        [StringLength(50)]
        public string item_code { get; set; }

		  public int minimum_qty { get; set; }

		  public int maximum_qty { get; set; }

		  public decimal unit_price { get; set; }
    }
}
