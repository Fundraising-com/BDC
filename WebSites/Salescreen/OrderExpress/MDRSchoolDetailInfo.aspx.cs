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
using dataDef = QSPForm.Common.DataDef.CMDRTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    ///<summary>FMDetailInfo: container for the FM Info display control</summary>
    public partial class MDRSchoolDetailInfo : BaseWebForm {
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

        string sMDRPID = "";

        protected override void InitControl() {
            this.AppItem = QSPForm.Business.AppItem.MDR_Detail;
            base.HiddenChange = hidChange;
            base.LabelInstruction = lblInstruction;
            base.LabelMessage = lblMessage;
            base.InitControl();
        }

        protected void Page_Load(object s, System.EventArgs e) {
            if (!IsPostBack) {
                if (Request.QueryString["MDRPID"] != null) {
                    sMDRPID = Request.QueryString["MDRPID"].ToString();
                }
                QSPForm.Business.MDRSystem mdrSys = new QSPForm.Business.MDRSystem();
                dataDef dTbl = mdrSys.SelectOne(sMDRPID);
                MDRSchoolInfo1.DataSource = dTbl;
                MDRSchoolInfo1.DataBind();
            }
        }
    }
}