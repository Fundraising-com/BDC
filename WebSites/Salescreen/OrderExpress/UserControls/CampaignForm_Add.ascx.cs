using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for OrderForm_Add.
    /// </summary>
    public partial class CampaignForm_Add : System.Web.UI.UserControl {
        private CommonUtility util = new CommonUtility();
        protected OrderData orderData;
        private const string ORDER_DATA = "OrderData";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here			
            if (!IsPostBack) {
            }
            else {
            }
            //			string NoMenu = Convert.ToInt32(QSPForm.Business.AppItem.AccSelector).ToString();
            //			util.SetJScriptForOpenSelector(imgBtnSelect,txtAccountID,txtOrganizationName,"CampaignSelector",800,700);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {
            this.ViewState[ORDER_DATA] = orderData;
        }
    }
}