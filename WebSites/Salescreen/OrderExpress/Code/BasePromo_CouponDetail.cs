//===========================================================================
// This file was generated as part of an ASP.NET 2.0 Web project conversion.
// This code file 'App_Code\Migrated\Stub_Promo_CouponDetail_ascx_cs.cs' was created and contains an abstract class 
// used as a base class for the class 'Migrated_Promo_CouponDetail' in file 'Promo_CouponDetail.ascx.cs'.
// This allows the the base class to be referenced by all code files in your project.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PromoCouponData;

namespace QSP.OrderExpress.Web.Code {
    public class BasePromo_CouponDetail : BaseWebFormControl {
        public const string Promo_Coupon_ID = "Promo_Coupon_id";
        private DataSet _d;

        public BasePromo_CouponDetail() {
            QSPForm.Common.DataDef.PromoCouponTable dtbl = new PromoCouponTable();
            DataRow r = dtbl.NewRow();
            dtbl.Rows.Add(r);
            _d = new DataSet();
            _d.Tables.Add(dtbl);
        }

        public DataSet DataSource {
            get { return _d; }
            set { _d = value; }
        }

        virtual public DataSet UpdatedDataSource {
            get { throw new NotImplementedException(); }
        }

        override public void DataBind() {
            throw new NotImplementedException();
        }
    }
}