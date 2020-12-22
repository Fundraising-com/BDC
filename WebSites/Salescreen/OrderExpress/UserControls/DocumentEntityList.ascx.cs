using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.DocumentEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for DocumentEntityList.
    /// </summary>
    public partial class DocumentEntityList : BaseWebSubFormControl {
        private const string DEFAULT_SORT = DocumentEntityTable.FLD_PKID + " ASC";
        protected dataDef dTblDocumentEntity = new dataDef();
        protected System.Web.UI.WebControls.DropDownList ddlCampaignType;
        protected DataView DVDocument;
        protected System.Web.UI.WebControls.LinkButton lnkBtnDocumentEntity;
        protected System.Web.UI.WebControls.Label lblFMID;
        protected System.Web.UI.WebControls.Label lblEndDate;
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_STATUS = "Filter_State";
        private CommonUtility clsUtil = new CommonUtility();

        private const string CHKBOX_RECEIVED = "chkReceived";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            InitControl();
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
            this.dtgDocument.ItemCommand += new DataGridCommandEventHandler(dtgDocument_ItemCommand);
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVDocument = new DataView(dTblDocumentEntity);
            this.DataSource = DVDocument;
            this.MainDataGrid = dtgDocument;
            dtgDocument.DataKeyField = DocumentEntityTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;

        }
        #endregion

        protected override void LoadDataSourceGrid() {
            ///	Call the appropriate Class from the Biz layer

            QSPForm.Business.DocumentEntitySystem docSys = new QSPForm.Business.DocumentEntitySystem();

            string sCriteria = this.dtgDocument.FilterExpression;

            switch (this.dtgDocument.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }
            int DocStatus = Convert.ToInt32(ViewState[FILTER_STATUS]);
            int DocType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            if (DocStatus == 0)
                dTblDocumentEntity = docSys.SelectAll_Search(this.dtgDocument.SearchMode, sCriteria, 0, 0, DocType);
            else
                dTblDocumentEntity = docSys.SelectAll_Search(this.dtgDocument.SearchMode, sCriteria, 0, 0, DocType, (DocStatus == 2));

            DVDocument = new DataView();
            DVDocument.Table = dTblDocumentEntity;

            DVDocument.Sort = this.dtgDocument.SortExpression;
            lblTotal.Text = "Number of Document(s) : " + DVDocument.Count.ToString();
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            clsUtil.SetDocumentTypeDropDownList(ddlDocumentType, true);
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlDocumentType.SelectedItem.Value);
            ViewState[FILTER_STATUS] = ddlStatus.SelectedItem.Value;

            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_TYPE] = Convert.ToInt32(ddlDocumentType.SelectedItem.Value);
            ViewState[FILTER_STATUS] = ddlStatus.SelectedItem.Value;
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    //					string sIDName = DocumentEntityDetailInfo.ORG_ID;
                    //					clsUtil.SetJScriptForOpenDetail(e.Item, AppItem.DocumentEntityDetailInfo, sIDName, sID, 0, 0);
                    //					
                    //					ImageButton imgBtnDetail = (ImageButton) e.Item.FindControl("imgBtnDetail");
                    //					if (imgBtnDetail != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(imgBtnDetail, AppItem.DocumentEntityDetail, sIDName, sID, 0,0);
                    //					}
                    //					LinkButton lnkBtnDocumentEntity = (LinkButton) e.Item.FindControl("lnkBtnDocumentEntity");
                    //					if (lnkBtnDocumentEntity != null)
                    //					{
                    //						clsUtil.SetJScriptForOpenDetail(lnkBtnDocumentEntity, AppItem.DocumentEntityDetail, sIDName, sID, 0,0);
                    //					}
                }
            }
        }

        private void imgBtnRefresh_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            QSPFormSearchModule.ApplySearch();
        }

        private void dtgDocument_ItemCommand(object source, DataGridCommandEventArgs e) {
            if (e.CommandName == "RECEIVED") {
                SetToReceived(e.Item);
            }
            if (e.CommandName == "APPROVE") {
                SetToApproved(e.Item);
            }
        }

        private void SetToReceived(DataGridItem e) {
            try {
                if (!((CheckBox)e.FindControl(CHKBOX_RECEIVED)).Checked) {

                    DocumentEntitySystem docSys = new DocumentEntitySystem();
                    //DocumentEntityTable dtbl = docSys.SelectOne(Convert.ToInt32(((Label)e.FindControl("lblID")).Text));
                    int ID = Convert.ToInt32(dtgDocument.DataKeys[e.ItemIndex].ToString());
                    int EntityTypeID = Convert.ToInt32(((Label)e.FindControl("lblEntityTypeID")).Text);
                    DocumentEntityTable dtbl = docSys.SelectOne(ID, EntityTypeID);
                    if (dtbl.Rows.Count > 0) {
                        bool newValue = !((CheckBox)e.FindControl(CHKBOX_RECEIVED)).Checked;
                        //					dtbl.Rows[0][dataDef.FLD_APPROVED] = newValue;
                        //					docSys.Update((DocumentEntityTable)dtbl);
                        clsUtil.UpdateRow(dtbl.Rows[0], dataDef.FLD_APPROVED, newValue.ToString());
                        clsUtil.UpdateRow(dtbl.Rows[0], dataDef.FLD_UPDATE_USER_ID, this.Page.UserID.ToString());
                        docSys.Update(dtbl);
                        int EntityID = Convert.ToInt32(dtbl.Rows[0][dataDef.FLD_ENTITY_ID]);
                        /*	if(EntityTypeID == Business.EntityType.TYPE_ACCOUNT)
                            {
                                AccountSystem accSys = new AccountSystem();
                                accSys.Refresh(EntityID, this.Page.UserID);							
                            }
                            else if(EntityTypeID == Business.EntityType.TYPE_CREDIT_APPLICATION)
                            {
                                CreditApplicationSystem crdSys = new CreditApplicationSystem();
                                crdSys.Refresh(EntityID, this.Page.UserID);
                            }	*/

                        this.LoadDataSourceGrid();
                        this.dtgDocument.SelectedIndex = -1;
                        this.dtgDocument.DataBind();
                    }
                    else {
                        this.Page.SetPageMessage("This document can not be found");
                    }
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        private void SetToApproved(DataGridItem e) {
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }

        public Label SearchName {
            get {
                return QSPFormSearchModule.LabelSearchByAlpha;
            }
            set {
                QSPFormSearchModule.LabelSearchByAlpha = value;
            }
        }
    }
}