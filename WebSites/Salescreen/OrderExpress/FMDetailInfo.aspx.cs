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
using dataDef = QSPForm.Common.DataDef.CUserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    ///<summary>FMDetailInfo: container for the FM Info display control</summary>
    public partial class FMDetailInfo : BaseWebForm {
        #region auto-generated code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitControl();
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {

        }
        #endregion auto-generated code

        string sFMID = "";

        protected void Page_Load(object s, System.EventArgs e) {
            if (!IsPostBack) {
                if (Request.QueryString["FMID"] != null) {
                    sFMID = Request.QueryString["FMID"].ToString();
                }
                this.FMInfo_Control1.FMID = sFMID;
                QSPForm.Business.CUserSystem fmSys = new QSPForm.Business.CUserSystem();
                dataDef dTblFM = fmSys.SelectOne(sFMID);
                FMInfo_Control1.DataSource = dTblFM;
                FMInfo_Control1.DataBind();
            }
        }

        protected override void InitControl() {
            this.AppItem = QSPForm.Business.AppItem.FM_Detail;
            //base.HiddenChange = hidChange;
            //base.LabelInstruction = lblInstruction;
            //base.LabelMessage = lblMessage;
            base.InitControl();

        }
    }
}