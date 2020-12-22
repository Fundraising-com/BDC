using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using QSPForm.Business;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CUserTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CampaignSelector.
    /// </summary>
    public partial class FMSelector : BaseWebSubFormControl {
        private const string DEFAULT_SORT = dataDef.FLD_FSM_NAME;
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
            //this.PreRender +=new EventHandler(Page_PreRender);
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
            QSPForm.Business.CUserSystem objSys = new QSPForm.Business.CUserSystem();

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
            dTblList = objSys.SelectAllFM_Search(dtgList.SearchMode, sCriteria);

            DVList = new DataView(dTblList);
            DVList.Sort = this.dtgList.SortExpression;
            //Resynchronize the DataSource
            base.DataSource = DVList;

            lblTotal.Text = "Number of FM(s) : " + DVList.Count.ToString();
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                //Nothing for now
                //				string sValueField = "FMNumber";
                //				//string sTextField = dataDef.FLD_NAME;
                //				String sID = "0";	
                //				sID = ((DataRowView)e.Item.DataItem).Row[sValueField].ToString();	
                //				clsUtil.SetJScriptForOpenDetail(e.Item,"CampaignDetail",CampaignDetail.CAMP_ID,sID,0,0,"OnDblClick");				
            }
        }

        private void dtgList_ItemCommand(object source, DataGridCommandEventArgs e) {

            if (e.CommandName.ToLower() == "select") {
                // Get FM values
                string SelectedFMId = ((Label)e.Item.FindControl("lblID")).Text;
                string SelectedFMName = ((HyperLink)e.Item.FindControl("hypLnkName")).Text;

                #region Set the value to the second "Sales to" FM field

                // This happens only when selecting the first "To FM" value
                if (IDRefCtrl.Contains("txtFMID1")) {
                    // Get the control names
                    string FMID2Control = IDRefCtrl.Replace("txtFMID1", "txtFMID2");
                    string FMName2Control = NameRefCtrl.Replace("txtFMName1", "txtFMName2");

                    // Generate javascript to assign values
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<script language=javascript> \n");
                    sb.Append("<!-- \n");
                    sb.Append("opener.document.getElementById('" + FMID2Control + "').value = \"" + SelectedFMId + "\"; \n");
                    sb.Append("opener.document.getElementById('" + FMName2Control + "').value = \"" + SelectedFMName + "\"; \n");
                    sb.Append("//--> \n");
                    sb.Append("</script> \n");

                    this.Page.RegisterClientScriptBlock("AssignSalesToFM", sb.ToString());
                }

                #endregion

                // Set closing script
                this.Page.RegisterClientScriptBlock("scriptClose", clsUtil.GetJScriptForCloseSelector(SelectedFMId, SelectedFMName, IDRefCtrl, NameRefCtrl));

                int a = 1;

            }
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}