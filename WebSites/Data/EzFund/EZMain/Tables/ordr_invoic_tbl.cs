using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GA.BDC.Data.EzFund.EZMain.Tables
{
    [Table("ORDR_INVOIC_TBL")]
    public partial class ordr_invoic_tbl
    {
        [Key, Dapper.Contrib.Extensions.Key]
        public int ORDR_ID { get; set; }

        [StringLength(10)]
        public string ORDR_TYPE_CDE { get; set; }

        public int ORG_ID { get; set; }

        [Required]
        public int CPGN_ID { get; set; }

        [Required]
        public bool PRIM_INVOIC_FLG { get; set; }

        public bool? PO_REQD_FLG { get; set; }

        public decimal? STAX_AMT { get; set; }

        public decimal? SHIP_CHRG_CALC_AMT { get; set; }

        public decimal? SHIP_CHRG_OVRD_AMT { get; set; }

        public decimal? TOTL_CHRG_AMT { get; set; }

        [StringLength(10)]
        public string PTYP_CDE { get; set; }

        [StringLength(20)]
        public string PMT_REF_NBR { get; set; }

        public DateTime? PMT_CCRD_EXPIRE_DTE { get; set; }

        public int? PMT_APVL_FLG { get; set; }

        [StringLength(40)]
        public string BILL_ADDR_1_TXT { get; set; }

        [StringLength(40)]
        public string BILL_ADDR_2_TXT { get; set; }

        [StringLength(40)]
        public string BILL_ADDR_3_TXT { get; set; }

        [StringLength(30)]
        public string BILL_CITY_NME { get; set; }

        [StringLength(10)]
        public string BILL_ST_CDE { get; set; }

        [StringLength(10)]
        public string BILL_ZIP_CDE { get; set; }

        [StringLength(40)]
        public string CTCT_NME { get; set; }

        [StringLength(20)]
        public string CTCT_PH_1_NBR { get; set; }

        [StringLength(20)]
        public string CTCT_PH_2_NBR { get; set; }

        [StringLength(50)]
        public string CTCT_EML_TXT { get; set; }

        [StringLength(40)]
        public string ALT_CTCT_NME { get; set; }

        [StringLength(20)]
        public string ALT_CTCT_PH_1_NBR { get; set; }

        [StringLength(20)]
        public string ALT_CTCT_PH_2_NBR { get; set; }

        [StringLength(50)]
        public string ALT_CTCT_EML_TXT { get; set; }

        [StringLength(40)]
        public string PDCT_SHIP_ADDR_1_TXT { get; set; }

        [StringLength(40)]
        public string PDCT_SHIP_ADDR_2_TXT { get; set; }

        [StringLength(40)]
        public string PDCT_SHIP_ADDR_3_TXT { get; set; }

        [StringLength(30)]
        public string PDCT_SHIP_CITY_NME { get; set; }

        [StringLength(10)]
        public string PDCT_SHIP_ST_CDE { get; set; }

        [StringLength(10)]
        public string PDCT_SHIP_ZIP_CDE { get; set; }

        public DateTime? PDCT_SHIP_DTE { get; set; }

        [StringLength(16)]
        public string PDCT_SHIP_VIA_CDE { get; set; }

        [StringLength(30)]
        public string PDCT_SHIP_TRACK_NBR { get; set; }

        [StringLength(255)]
        public string PDCT_SHIP_ADDL_TXT { get; set; }

        [StringLength(40)]
        public string PRZP_SHIP_ADDR_1_TXT { get; set; }

        [StringLength(40)]
        public string PRZP_SHIP_ADDR_2_TXT { get; set; }

        [StringLength(40)]
        public string PRZP_SHIP_ADDR_3_TXT { get; set; }

        [StringLength(30)]
        public string PRZP_SHIP_CITY_NME { get; set; }

        [StringLength(10)]
        public string PRZP_SHIP_ST_CDE { get; set; }

        [StringLength(10)]
        public string PRZP_SHIP_ZIP_CDE { get; set; }

        public DateTime? PRZP_SHIP_DTE { get; set; }
        
        [StringLength(16)]
        public string PRZP_SHIP_VIA_CDE { get; set; }

        [StringLength(30)]
        public string PRZP_SHIP_TRACK_NBR { get; set; }

        [StringLength(255)]
        public string PRZP_SHIP_ADDL_TXT { get; set; }

        [StringLength(1000)]
        public string DLVY_CMNT_TXT { get; set; }

        [StringLength(10)]
        public string SLSP_CDE { get; set; }

        public DateTime? AR_POST_DTE { get; set; }

        public bool SENT_TO_ACTG_FLG { get; set; }

        public bool SENT_TO_ORG_FLG { get; set; }

        public DateTime CREA_DTE { get; set; }

        [StringLength(10)]
        public string CREA_PRSN_CDE { get; set; }

        public DateTime LAST_MODF_DTE { get; set; }

        [StringLength(10)]
        public string LAST_MODF_PRSN_CDE { get; set; }

        public DateTime LAST_STAT_DTE { get; set; }

        [StringLength(16)]
        public string LAST_STAT_CDE { get; set; }

        public int? ORDR_SRC_LKUP_TBL_ID { get; set; }

        [StringLength(10)]
        public string ORDR_SRC_CDE { get; set; }

        [StringLength(255)]
        public string ORDR_REF { get; set; }
    }
}
