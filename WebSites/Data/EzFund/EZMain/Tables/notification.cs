using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("notification")]
    public partial class notification
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int id { get; set; }

        [Required]
        public int external_id { get; set; }

        [Required]
        public int type { get; set; }

        [StringLength(250)]
        public string email { get; set; }

        [StringLength(250)]
        public string extra_data { get; set; }

        [Required]
        public DateTime created_on { get; set; }
    }
}
