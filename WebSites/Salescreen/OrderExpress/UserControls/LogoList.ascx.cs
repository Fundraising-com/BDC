using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Common;
using QSPForm.Business;
using QSP.WebControl;
using System.Text.RegularExpressions;
using dataDef = QSPForm.Common.DataDef.LogoTable;
//using dataDef = QSPForm.Common.DataDef.ToDoTable; <-- DataDef pour LogoLogo
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoList</summary>
    public partial class LogoList : BaseWebSubFormControl {
        private const string FILTER_SUBDIVISION = "Filter_Subdivision";
        private const string FILTER_LOGO_TYPE = "Filter_Logo_Type";
        private const string FILTER_DISPLAY_STATUS = "Filter_Display_Status";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private const string FILTER_RPT_TO = "Filter_Reported_To";
        //		private const string FM_ID = "fm_id";
        private const string DEFAULT_SORT = LogoTable.FLD_PKID + " DESC";

        //private const int ImgBtnDetail_GRID_COLUMN = 0;
        private const int lblID_GRID_COLUMN = 1;
        //private const int Name_GRID_COLUMN = 2;
        //private const int FSM_ID_GRID_COLUMN = 4;
        private const int FSM_ID_GRID_COLUMN = 4;
        private const int FSM_NAME_GRID_COLUMN = 5;
        private const int FAVORITE_GRID_COLUMN = 7;
        private static Regex _isNumber = new Regex(@"^\d+$");
        protected DataView DVLogo;
        protected dataDef dTblLogo = new dataDef();
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            AddJavascript();
            InitializeUI();
        }

        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>

        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
            this.dtgLogo.ItemCommand += new DataGridCommandEventHandler(dtgLogo_ItemCommand);
            base.OnInit(e);
        }

        void dtgLogo_ItemCommand(object source, DataGridCommandEventArgs e) {
            try {
                if (e.CommandName == "AddToFavorite") {
                    if (CountFavorite() < 25) {
                        ((ImageButton)e.Item.FindControl("imgBtnAddToFavorite")).Visible = false;
                        ((ImageButton)e.Item.FindControl("imgBtnRemoveFromFavorite")).Visible = true;
                        int id = Convert.ToInt32(((Label)e.Item.FindControl("lblID")).Text);
                        QSPForm.Business.Favorite_LogoSystem fsys = new Favorite_LogoSystem();
                        fsys.InsertWithFMID(this.fm_id, id);
                        RefreshPageGrid();
                    }
                    else {
                        string x = "The maximum of 25 Favorite Logos has been reached. To proceed, please remove one Favorite Logo from Logo List";
                        this.Page.RegisterClientScriptBlock("AddToFavorite", "<script>alert('" + x + "')</script>");
                    }

                }
                if (e.CommandName == "RemoveFromFavorite") {
                    int id = Convert.ToInt32(((Label)e.Item.FindControl("lblID")).Text);
                    QSPForm.Business.Favorite_LogoSystem fsys = new Favorite_LogoSystem();
                    fsys.Delete(this.fm_id, id);
                    RefreshPageGrid();
                }

            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {

        }
        #endregion auto-generated, Initialization code

        private string fm_id {
            get {
                try {
                    if (this.Page.Role == AuthSystem.ROLE_FM) {
                        return this.Page.FMID;
                    }
                    else {
                        return txtFMID.Text.Trim();
                    }

                }
                catch {
                    return String.Empty;
                }
            }
        }

        private bool FavoriteColumnVisible {
            get {
                return dtgLogo.Columns[FAVORITE_GRID_COLUMN].Visible;
            }
            set {
                dtgLogo.Columns[FAVORITE_GRID_COLUMN].Visible = value;
            }
        }

        private QSPForm.Common.DataDef.Favorite_LogoTable FavoriteList {
            get {
                try { return (QSPForm.Common.DataDef.Favorite_LogoTable)this.ViewState["fav"]; }
                catch { return null; }
            }
            set {
                this.ViewState["fav"] = value;
            }
        }

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVLogo = new DataView(dTblLogo);
            this.DataSource = DVLogo;
            this.MainDataGrid = dtgLogo;
            dtgLogo.DataKeyField = LogoTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
            //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.LogoDetail, BaseLogoDetail.LOGO_ID, "0", 0,0);
            clsUtil.SetJScriptForOpenDetailNoCMS(hypLnkAddNew, "LogoDetail.aspx?", BaseLogoDetail.LOGO_ID, "0", 0, 0);
        }

        private void InitializeUI() {
            bool isFM = (Page.Role == AuthSystem.ROLE_FM);
            //FSMIDColumnVisible = !isFM;
            //FSMNameColumnVisible = !isFM;

            //tdFilterFMReportedTo.Visible = isFM;
            if (isFM || (this.fm_id != String.Empty) || ddlLogoType.SelectedValue == "2")
                FavoriteColumnVisible = true;
            else
                FavoriteColumnVisible = false;

            FMSelector.Visible = (Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT);

            //imgbtnAddOrder.Visible = (this.Page.RightInsert);
        }

        protected override void FillFilter() {
            CommonUtility clsUtil = new CommonUtility();

            //clsUtil.SetRegionDropDownList(ddlRegion, true);
            clsUtil.SetUSSubdivisionDropDownList(ddlSubdivision, true);
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;

            ViewState[FILTER_LOGO_TYPE] = ddlLogoType.SelectedItem.Value;

            ViewState[FILTER_DISPLAY_STATUS] = ddlDisplayStatus.SelectedItem.Value;

            if (tdFilterFMReportedTo.Visible) {
                ViewState[FILTER_RPT_TO] = chkReportedTo.Checked;
            }
            else {
                ViewState[FILTER_RPT_TO] = false;
            }

            base.FillFilter();
        }

        public override void BindGrid() {
            this.Page.Validate();
            if (this.Page.IsValid)
                base.BindGrid();
        }

        private void RefreshPageGrid() {
            QSPForm.Business.Favorite_LogoSystem fsys = new Favorite_LogoSystem();
            this.FavoriteList = fsys.SelectLogoIDByFM_ID(this.fm_id);

            foreach (DataGridItem item in this.dtgLogo.Items) {
                AdjustButton(item);
            }
        }

        protected override void LoadDataSourceGrid() {
            //find favorite list by fm's user_id       
            QSPForm.Business.Favorite_LogoSystem fsys = new Favorite_LogoSystem();
            this.FavoriteList = fsys.SelectLogoIDByFM_ID(this.fm_id);
            //this.DefaultFavoriteList = fsys.SelectAllDefault();

            if (!IsPostBack)
                clsUtil.SetImageCategoryDropDownList(ddlCategory, true);
            //	Call the appropriate Class from the Biz layer
            QSPForm.Business.LogoSystem logoSys = new LogoSystem();
            string sCriteria = this.dtgLogo.FilterExpression;

            switch (this.dtgLogo.SearchMode) {
                case 5:
                    bool IsInt = IsInteger(sCriteria);

                    if (!IsInt)
                        sCriteria = string.Empty;
                    break;

                default: {
                        sCriteria = "%" + sCriteria + "%";
                        break;
                    }
            }

            string SubdivisionID = ViewState[FILTER_SUBDIVISION].ToString();
            int logoType = Convert.ToInt32(ViewState[FILTER_LOGO_TYPE]);
            int DisplayStatus = Convert.ToInt32(ViewState[FILTER_DISPLAY_STATUS]);
            bool IncludeFMReportedTo = Convert.ToBoolean(ViewState[FILTER_RPT_TO]);
            int Category = Convert.ToInt32(ddlCategory.SelectedValue);

            dTblLogo = logoSys.SelectAll_Search(this.dtgLogo.SearchMode, sCriteria, logoType, DisplayStatus, this.fm_id, Category);

            DVLogo = new DataView();
            DVLogo.Table = dTblLogo;

            DVLogo.Sort = this.dtgLogo.SortExpression;
            lblTotal.Text = "Number of Logo(s) : " + DVLogo.Count.ToString();
        }

        public static bool IsInteger(string theValue) {
            Match m = _isNumber.Match(theValue);
            return m.Success;
        } //IsInteger

        private void QSPFormSearchModule_OnSearch(object sender, SearchModuleClickedArgs e) {
            ViewState[FILTER_SUBDIVISION] = ddlSubdivision.SelectedItem.Value;
            ViewState[FILTER_LOGO_TYPE] = ddlLogoType.SelectedItem.Value;
            ViewState[FILTER_DISPLAY_STATUS] = ddlDisplayStatus.SelectedItem.Value;
            if (tdFilterFMReportedTo.Visible) {
                ViewState[FILTER_RPT_TO] = chkReportedTo.Checked;
            }
            else {
                ViewState[FILTER_RPT_TO] = false;
            }
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    ((ImageButton)e.Item.FindControl("imgBtnDetail")).ImageUrl = QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath +
                                                                                    ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString() + "." +
                                                                                    QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;

                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = LogoDetailInfo.LOGO_ID;
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0,0);
                    //clsUtil.SetJScriptForOpenDetail(((ImageButton)e.Item.FindControl("imgBtnDetail")), QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0, 0);
                    //clsUtil.SetJScriptForOpenDetail(((Label)e.Item.FindControl("Name")), QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0, 0);
                    //clsUtil.SetJScriptForOpenDetail(((Label)e.Item.FindControl("Description")), QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0, 0);
                    //clsUtil.SetJScriptForOpenDetail(((Label)e.Item.FindControl("lblLogoInformation")), QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0, 0);
                    //clsUtil.SetJScriptForOpenDetail(((Label)e.Item.FindControl("lblID")), QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(((ImageButton)e.Item.FindControl("imgBtnDetail")), "LogoDetailInfo.aspx?", sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(((Label)e.Item.FindControl("Name")), "LogoDetailInfo.aspx?", sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(((Label)e.Item.FindControl("Description")), "LogoDetailInfo.aspx?", sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(((Label)e.Item.FindControl("lblLogoInformation")), "LogoDetailInfo.aspx?", sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(((Label)e.Item.FindControl("lblID")), "LogoDetailInfo.aspx?", sIDName, sID, 0, 0);
                    //remove add button if logo is already selected
                    AdjustButton(e.Item);
                    AdjustInformation(e.Item);
                }
            }
        }

        private void AdjustButton(DataGridItem item) {
            if (FavoriteColumnVisible) {

                string id = ((Label)item.FindControl("lblID")).Text;

                int index = -1;

                for (int i = 0; i < this.FavoriteList.Rows.Count; i++) {
                    if (this.FavoriteList.Rows[i][Favorite_LogoTable.FLD_LOGO_ID].ToString() == id) {
                        index = i;
                        break;
                    }
                }

                if (index != -1) {
                    if (this.FavoriteList.Rows[index][QSPForm.Common.DataDef.Favorite_LogoTable.FLD_FIELD_SALES_MANAGER_ID].ToString() == String.Empty) {
                        ((ImageButton)item.FindControl("imgBtnAddToFavorite")).Visible = false;
                        ((Label)item.FindControl("lblDefaultFavorite")).Visible = true;
                        ((ImageButton)item.FindControl("imgBtnRemoveFromFavorite")).Visible = false;
                    }
                    else {
                        ((ImageButton)item.FindControl("imgBtnAddToFavorite")).Visible = false;
                        ((Label)item.FindControl("lblDefaultFavorite")).Visible = false;
                        ((ImageButton)item.FindControl("imgBtnRemoveFromFavorite")).Visible = true;
                    }
                }
                else {
                    ((ImageButton)item.FindControl("imgBtnAddToFavorite")).Visible = true;
                    ((Label)item.FindControl("lblDefaultFavorite")).Visible = false;
                    ((ImageButton)item.FindControl("imgBtnRemoveFromFavorite")).Visible = false;
                }
            }
        }

        private void AdjustInformation(DataGridItem item) {
            string fmid = ((Label)item.FindControl("lblFMID")).Text;
            if (fmid == String.Empty)
                ((Label)item.FindControl("lblLogoInformation")).Text = "QSP Image";
            else if (this.fm_id != String.Empty)
                ((Label)item.FindControl("lblLogoInformation")).Text = "Personal Image";
            else {
                ((Label)item.FindControl("lblLogoInformation")).Text = ((Label)item.FindControl("lblFMID")).Text + " - " + ((Label)item.FindControl("lblFMName")).Text;
            }
        }

        private void AddJavascript() {
            txtFMName.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID.Attributes.Add("onfocus", "javascript:window.focus();");
            // clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, QSPForm.Business.AppItem.FMSelector, 0, 0);
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, "FMSelector.aspx", "LogoDetail", 0, 0, null);
        }

        private int CountFavorite() {
            int i = 0;
            foreach (DataRow row in FavoriteList.Rows) {
                if (row[QSPForm.Common.DataDef.Favorite_LogoTable.FLD_FIELD_SALES_MANAGER_ID].ToString() != String.Empty)
                    i++;
            }
            return i;
        }

        public DropDownList SearchCriteria {
            get {
                return QSPFormSearchModule.DropDownListSearchBy;
            }
        }
    }
}