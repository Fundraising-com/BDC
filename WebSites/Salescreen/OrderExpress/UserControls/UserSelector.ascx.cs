using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.UserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class UserSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_LAST_NAME;
        protected dataDef dTblList = new dataDef();
        protected DataView DVList;
        private String IDRefCtrl = "";
        private String NameRefCtrl = "";
        private CommonUtility clsUtil = new CommonUtility();

        override protected void OnLoad(System.EventArgs e) {
            // Put user code to initialize the page here
            if (Request["IDRefCtrl"] != null) {
                IDRefCtrl = Request["IDRefCtrl"].ToString();
            }

            if (Request["NameRefCtrl"] != null) {
                NameRefCtrl = Request["NameRefCtrl"].ToString();
            }
            base.OnLoad(e);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            //this.PreRender += new System.EventHandler(this.Page_PreRender);
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVList = new DataView(dTblList);
            this.DataSource = DVList;
            this.MainDataGrid = dtgList;
            dtgList.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

            this.dtgList.ItemCommand += new DataGridCommandEventHandler(dtgList_ItemCommand);
        }

        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer
            //			if (!IsPostBack)
            //				dtgList.FilterExpression = "A";
            QSPForm.Business.UserSystem objSys = new QSPForm.Business.UserSystem();

            string sCriteria = dtgList.FilterExpression;
            switch (this.dtgList.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }
            dTblList = objSys.SelectAll_Search(dtgList.SearchMode, sCriteria);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of User(s) : " + DVList.Count.ToString();
        }

        private void dtgList_ItemCommand(object source, DataGridCommandEventArgs e) {

            if (e.CommandName.ToLower() == "select") {
                string sID = "";
                string sName = "";
                sID = ((Label)e.Item.FindControl("lblID")).Text;
                sName = ((Label)e.Item.FindControl("lblUserName")).Text;
                this.Page.RegisterClientScriptBlock("scriptClose", clsUtil.GetJScriptForCloseSelector(sID, sName, IDRefCtrl, NameRefCtrl));
            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}