using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace QSP.OrderExpress.Web {
    public partial class MainMaster : System.Web.UI.MasterPage {

        protected void Page_Load(object sender, EventArgs e) {

        }

        public string lblMessage {
            get {
                return lblMessage1.Text;
            }
            set {
                lblMessage1.Text = value;
            }
        }

        public System.Web.UI.WebControls.Label LabelMessage1 {
            get {
                return lblMessage1;
            }
            set {
                lblMessage1 = value;
            }
        }

        public bool ValSummaryVisibility {
            get {
                return ValSum.Visible;
            }
            set {
                ValSum.Visible = value;
            }
        }
    } 
}