using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("AR_TRNS_TBL")]
    public partial class ar_trns_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int TRNS_ID  { get; set; }
        public string TRNS_TYPE_CDE { get; set; }
        public DateTime? TRNS_DTE { get; set; }
        public int? ORG_ID { get; set; }
        public int? ORDR_ID { get; set; }
        public string OPPOS_ACCT_NBR { get; set; }
        public string CASH_BATCH_NBR { get; set; }
        public string PMT_METH_TYPE_CDE { get; set; }
        public string PMT_METH_REF_NBR { get; set; }
        public decimal? TRNS_AMT { get; set; }
        public int? JE_NBR { get; set; }
        public DateTime? QBKS_POST_DTE { get; set; }
        public DateTime? LAST_MODF_DTE { get; set; }
        public string LAST_MODF_PRSN_CDE { get; set; }
        public int? PAYMENT_STATUS_ID { get; set; }
        public int? EXT_PAYMENT_ID { get; set; }

    }
}
