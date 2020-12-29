using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
	[Table("page_route_mapper")]
	public partial class page_route_mapper
	{
		[Key, Required]
		public int id { get; set; }
		[Required, MaxLength]
		public string source { get; set; }
		[Required, MaxLength]
		public string destination { get; set; }
		[Required]
		public DateTime created { get; set; }
		[Required]
		public bool enabled { get; set; }
	}
}
