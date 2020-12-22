using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using QSPForm.Common;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for AuditControlInfo.
    /// </summary>
    public partial class AuditControlInfo : BaseWebUserControl {
        private int c_ParentID = 0;
        private int c_ParentType = 0;
        bool c_HideHistoryLink = true;
        protected DataTable dTblAudit;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here		
            //Set View History Button
            CommonUtility clsUtil = new CommonUtility();
            if (c_ParentType == EntityType.TYPE_ACCOUNT) {
                //  clsUtil.SetJScriptForOpenDetail(imgBtnViewHistory, AppItem.AccountStatusChangeList, "AccountID", c_ParentID.ToString(), 0, 0);		
                clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnViewHistory, "AccountStatusChangeList.aspx?", "AccountID", c_ParentID.ToString(), 0, 0);
            }
            else if (c_ParentType == EntityType.TYPE_ORDER_BILLING) {
                //  clsUtil.SetJScriptForOpenDetail(imgBtnViewHistory, AppItem.OrderStatusChangeList, "OrderID", c_ParentID.ToString(), 0, 0);
                clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnViewHistory, "OrderStatusChangeList.aspx?", "OrderID", c_ParentID.ToString(), 0, 0);
            }
            else if (c_ParentType == EntityType.TYPE_PROGRAM_AGREEMENT) {
                clsUtil.SetJScriptForOpenDetailNoCMS(imgBtnViewHistory, "ProgramAgreementStatusChangeList.aspx?", "ProgramAgreementID", c_ParentID.ToString(), 0, 0);
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void BindForm() {
            if (dTblAudit != null) {
                if (dTblAudit.Rows.Count > 0) {
                    DataRow row = dTblAudit.Rows[0];

                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_CREATE_DATE) > -1) {
                        lblCreateDate.Text = row[CommonTable.FLD_CREATE_DATE].ToString();
                    }
                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_CREATE_LAST_NAME) > -1) {
                        lblCreateName.Text = row[CommonTable.FLD_CREATE_LAST_NAME].ToString() +
                                             " " + row[CommonTable.FLD_CREATE_FIRST_NAME].ToString();
                    }
                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_UPDATE_DATE) > -1) {
                        lblUpdateDate.Text = row[CommonTable.FLD_UPDATE_DATE].ToString();
                    }
                    if (dTblAudit.Columns.IndexOf(CommonTable.FLD_UPDATE_LAST_NAME) > -1) {
                        lblUpdateName.Text = row[CommonTable.FLD_UPDATE_LAST_NAME].ToString() +
                                             " " + row[CommonTable.FLD_UPDATE_FIRST_NAME].ToString();
                    }
                }
            }
        }

        public DataTable DataSource {
            get {
                return dTblAudit;
            }
            set {
                dTblAudit = value;
            }
        }

        public int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public int ParentType {
            //Identify on wich we have to do our operation
            //0= Nothing (direct to the postal address table)
            //1= Organization
            //2= Account
            //3= Campaign
            //4= Order
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
            }
        }

        public bool HideHistoryLink {
            get {
                return c_HideHistoryLink;
            }
            set {
                c_HideHistoryLink = value;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                imgBtnViewHistory.Visible = !c_HideHistoryLink;

            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}