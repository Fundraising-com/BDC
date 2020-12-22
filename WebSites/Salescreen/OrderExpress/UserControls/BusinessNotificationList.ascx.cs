using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using QSP.WebControl;
using dataDef = QSPForm.Common.DataDef.BusinessNotificationTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>BusinessNotificationList</summary>
    public partial class BusinessNotificationList : BaseWebSubFormControl {

        private const string DEFAULT_SORT = BusinessNotificationTable.FLD_CREATE_DATE + " DESC";
        protected BusinessNotificationTable dTblNote = new BusinessNotificationTable();
        protected DataView DVNote;
        protected System.Web.UI.WebControls.ImageButton imgbtnAddBusinessNotification;
        private CommonUtility clsUtil = new CommonUtility();
        private const string FILTER_STATUS = "Filter_State";
        private const string FILTER_TYPE = "Filter_Type";
        private const string FILTER_ENTITY_TYPE = "Filter_Entity_Type";
        private string appItem = String.Empty;

        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>
        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            base.OnInit(e);
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(QSPFormSearchModule_OnSearch);
            this.Page.RefreshPage += new EventHandler(Page_RefreshPage);
        }

        void Page_RefreshPage(object sender, EventArgs e) {
            BindGrid();
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            this.DVNote = new DataView(dTblNote);
            this.DataSource = this.DVNote;
            this.MainDataGrid = this.dtgBusinessNotificationItems;
            this.dtgBusinessNotificationItems.DataKeyField = dataDef.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
        }
        #endregion auto-generated, Initialization code

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!Page.IsPostBack) {

                ////dtgBusinessNotificationItems.Columns[1].Visible = false;
                //if (this.Page.AppItem == QSPForm.Business.AppItem.MyNoteList)
                //{
                //    dtgBusinessNotificationItems.Columns[3].Visible = false;
                //}

                if (AppItem == "mynotelist") {
                    dtgBusinessNotificationItems.Columns[3].Visible = false;
                }

                //Manage the visibility of the Filter Note Type and the column in the grid    
                //dtgBusinessNotificationItems.Columns[3].Visible = (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT); 
                tblFilterNoteType.Visible = (this.Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT);
            }
            if (this.Page.Role >= AuthSystem.ROLE_ADMINISTRATOR) {
                hypLnkAddNew.Visible = true;
                //   clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.NoteDetail, BusinessNotificationDetailInfo.BIZNOTE_ID, "0", 0, 0);
                clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "BusinessNotificationDetail.aspx?", BusinessNotificationDetailInfo.BIZNOTE_ID, "0", 0, 0);
            }
            else
                hypLnkAddNew.Visible = false;
        }

        public string AppItem {
            get { return appItem; }
            set { appItem = value; }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        protected override void FillFilter() {
            ViewState[FILTER_STATUS] = ddlStatus.SelectedItem.Value;
            CommonUtility clsUtil = new CommonUtility();
            clsUtil.SetBizNotificationTypeDropDownList(ddlNoteType, true);
            ViewState[FILTER_TYPE] = ddlNoteType.SelectedItem.Value;
            ViewState[FILTER_ENTITY_TYPE] = ddlEntityType.SelectedItem.Value;
            base.FillFilter();
        }

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_STATUS] = ddlStatus.SelectedItem.Value;
            ViewState[FILTER_TYPE] = ddlNoteType.SelectedItem.Value;
            ViewState[FILTER_ENTITY_TYPE] = ddlEntityType.SelectedItem.Value;
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.BusinessNotificationSystem bizNoteSys = new QSPForm.Business.BusinessNotificationSystem();

            string sCriteria = this.dtgBusinessNotificationItems.FilterExpression;
            switch (this.dtgBusinessNotificationItems.SearchMode) {
                case 0: {
                        sCriteria = sCriteria + "%";
                        break;
                    }
                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            int BizNoteStatus = Convert.ToInt32(ViewState[FILTER_STATUS]);
            int ParamUserID = 0;
            //if (this.Page.AppItem == QSPForm.Business.AppItem.MyNoteList)
            //    ParamUserID = this.Page.UserID;

            if (AppItem == "mynotelist")
                ParamUserID = this.Page.UserID;

            int BizNoteType = Convert.ToInt32(ViewState[FILTER_TYPE]);
            int EntityTypeID = Convert.ToInt32(ViewState[FILTER_ENTITY_TYPE]);
            if (BizNoteStatus == 0)
                dTblNote = bizNoteSys.SelectAll_Search(dtgBusinessNotificationItems.SearchMode, sCriteria, ParamUserID, BizNoteType, EntityTypeID);
            else
                dTblNote = bizNoteSys.SelectAll_Search(dtgBusinessNotificationItems.SearchMode, sCriteria, ParamUserID, BizNoteType, EntityTypeID, (BizNoteStatus == 2));

            this.DVNote = new DataView(dTblNote);
            this.DVNote.Sort = this.dtgBusinessNotificationItems.SortExpression;
            this.DataSource = this.DVNote;
            lblTotal.Text = "Number of Note(s) : " + this.DVNote.Count.ToString();
        }

        protected override void OnItemCreated(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                HyperLink hypLnkSubject = (HyperLink)e.Item.FindControl("hypLnkSubject");
                System.Web.UI.WebControls.Image imgNote = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgNote");
                Label lblFromName = (Label)e.Item.FindControl("lblFromName");
                Label lbcomplete_date = (Label)e.Item.FindControl("lbcomplete_date");
                Label lbAssigned = (Label)e.Item.FindControl("lbAssigned");

                if (hypLnkSubject != null) {
                    sID = dtgBusinessNotificationItems.DataKeys[e.Item.ItemIndex].ToString();
                    string sIDName = BusinessNotificationDetailInfo.BIZNOTE_ID;
                    //  clsUtil.SetJScriptForOpenDetail(hypLnkSubject, QSPForm.Business.AppItem.NoteDetailInfo, sIDName, sID, 650, 700);
                    clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkSubject, "BusinessNotificationDetailInfo.aspx?", sIDName, sID, 650, 700);
                    clsUtil.SetJScriptForOpenDetailNoCMS(imgNote, "BusinessNotificationDetailInfo.aspx?", sIDName, sID, 650, 700);
                    clsUtil.SetJScriptForOpenDetailNoCMS(lblFromName, "BusinessNotificationDetailInfo.aspx?", sIDName, sID, 650, 700);
                    clsUtil.SetJScriptForOpenDetailNoCMS(lbcomplete_date, "BusinessNotificationDetailInfo.aspx?", sIDName, sID, 650, 700);
                    clsUtil.SetJScriptForOpenDetailNoCMS(lbAssigned, "BusinessNotificationDetailInfo.aspx?", sIDName, sID, 650, 700);
                }
            }

            if ((e.Item.ItemType == ListItemType.Header)) {
                CheckBox chkSelectedAll = (CheckBox)e.Item.FindControl("chkSelectedAll");
                if (chkSelectedAll != null) {
                    chkSelectedAll.Attributes.Add("onclick", "CheckedAll('" + chkSelectedAll.ClientID + "');");
                }
            }
        }

        protected void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                BusinessNotificationSystem bizSys = new BusinessNotificationSystem();

                foreach (DataGridItem dgItem in dtgBusinessNotificationItems) {
                    if ((dgItem.ItemType == ListItemType.Item) || (dgItem.ItemType == ListItemType.AlternatingItem)) {
                        String sID = "0";
                        sID = dtgBusinessNotificationItems.DataKeys[dgItem.ItemIndex].ToString();
                        CheckBox chkSelected = (CheckBox)dgItem.FindControl("chkSelected");
                        if (chkSelected != null) {
                            if (chkSelected.Checked) {
                                bizSys.DeleteOne(Convert.ToInt32(sID), this.Page.UserID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            BindGrid();
        }

        protected string GetNoteImageURL(string sBizNoteType, string sIsComplete) {
            string sImageURL = "~/images/MyNotes/msg_read.gif";
            if (sBizNoteType == QSPForm.Common.BizNotificationType.GENERAL_NOTIFICATION.ToString()) {
                sImageURL = "~/images/MyNotes/msg_read.gif";
                if (sIsComplete != "True") {
                    sImageURL = "~/images/MyNotes/msg_unread.gif";
                }
            }
            if (sBizNoteType == QSPForm.Common.BizNotificationType.TODO.ToString()) {
                sImageURL = "~/images/MyNotes/todo_complete.gif";
                if (sIsComplete != "True") {
                    sImageURL = "~/images/MyNotes/todo_incomplete.gif";
                }
            }
            if (sBizNoteType == QSPForm.Common.BizNotificationType.SYNCH_SYSTEM_ERROR.ToString() ||
                sBizNoteType == QSPForm.Common.BizNotificationType.SUPPLY_IMPORT_ERROR.ToString() ||
                sBizNoteType == QSPForm.Common.BizNotificationType.SYNCH_VALIDATION_ERROR.ToString()) {
                sImageURL = "~/images/MyNotes/error_msg_read.gif";
                if (sIsComplete != "True") {
                    sImageURL = "~/images/MyNotes/error_msg_unread.gif";
                }
            }
            return sImageURL;
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