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
using dataDef = QSPForm.Common.DataDef.LogoTable;
//using dataDef = QSPForm.Common.DataDef.ToDoTable; <-- DataDef pour LogoLogo
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    ///<summary>ToDoList</summary>
    public partial class FavoriteList : BaseWebSubFormControl {
        protected DataView DVLogo;
        private const string FILTER_SUBDIVISION = "Filter_Subdivision";
        private const string FILTER_IS_NATIONAL = "Filter_Is_National";
        private const string FILTER_DISPLAY_STATUS = "Filter_Display_Status";
        private const string PARAM_START = "Param_Start";
        private const string PARAM_END = "Param_End";
        private const string FILTER_RPT_TO = "Filter_Reported_To";
        //		private const string FM_ID = "fm_id";
        protected dataDef dTblLogo = new dataDef();
        private const string DEFAULT_SORT = LogoTable.FLD_PKID + " DESC";
        private CommonUtility clsUtil = new CommonUtility();

        protected void Page_Load(object sender, System.EventArgs e) {
            AddJavascript();
            if (Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                FMSelector.Visible = true;
            }
            else {
                FMSelector.Visible = false;
            }
        }

        #region auto-generated, Initialization code
        ///<summary>Required method for Designer support</summary>
        ///

        private int LogoCount {
            get { return Convert.ToInt32(this.ViewState["LogoCount"].ToString()); }
            set { this.ViewState["LogoCount"] = value; }
        }

        //private int UserID
        //{
        //    get
        //    {
        //        if (this.ViewState["UserID"] != null)
        //            return Convert.ToInt32(this.ViewState["UserID"]);
        //        else
        //            return this.Page.UserID;

        //    }
        //    set { this.ViewState["UserID"] = value; }
        //}

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
        private System.Collections.ArrayList Favorite {
            get {
                System.Collections.ArrayList al = new System.Collections.ArrayList();
                if (this.ViewState["favorite_list"] != null)
                    al = (System.Collections.ArrayList)this.ViewState["favorite_list"];
                return al;
            }
            set { this.ViewState["favorite_list"] = value; }
        }
        private System.Collections.ArrayList DefaultFavoriteList {
            get {
                System.Collections.ArrayList al = new System.Collections.ArrayList();
                if (this.ViewState["default_favorite_list"] != null)
                    al = (System.Collections.ArrayList)this.ViewState["default_favorite_list"];
                return al;
            }
            set { this.ViewState["default_favorite_list"] = value; }
        }

        override protected void OnInit(EventArgs e) {
            InitializeComponent();
            InitControl();
            //this.QSPFormSearchModule.OnSearch += new SearchModuleEventHandler(this.QSPFormSearchModule_OnSearch);
            this.dtgLogo.ItemCommand += new DataGridCommandEventHandler(dtgLogo_ItemCommand);
            this.PreRender += new EventHandler(FavoriteList_PreRender);
            base.OnInit(e);
        }

        void FavoriteList_PreRender(object sender, EventArgs e) {
            //override the one from basepage
            if (!IsPostBack) {
                string instruction;

                if (Page.Role >= AuthSystem.ROLE_FIELD_SUPPORT) {
                    instruction = "To update the Favorite List for an FSM, use the Select User feature; click Select button, then click Select button for that FSM.  The Favorite List is displayed on Step 9 – Personalization page.";
                    instruction += "<br><br>Note:  The Favorite List always contains 25 logos.  Therefore, if a logo is removed or deleted, a default logo will automatically populate the Favorite List in its place.";
                }
                else {
                    instruction = "To delete a logo from your Favorites List, click on REMOVE FROM FAVORITES button and it will automatically be deleted from this list. <br>";
                    instruction += "To add a new logo to your Favorites List, click on Logo List [Menu Bar] and follow the directions.<br>";
                    instruction += "<br>NOTE:  The Favorites List is limited to 25 logos.";
                }

                //((BaseWebForm)this.Parent.Parent.Parent).LabelInstruction.Text = instruction;
                this.Page.LabelInstruction.Text = instruction;
            }
        }

        void dtgLogo_ItemCommand(object source, DataGridCommandEventArgs e) {
            if (e.CommandName == "RemoveFromFavorite") {
                e.Item.Visible = false;
                int id = Convert.ToInt32(((Label)e.Item.FindControl("lblID")).Text);
                QSPForm.Business.Favorite_LogoSystem fsys = new Favorite_LogoSystem();
                fsys.Delete(this.fm_id, id);
                LogoCount--;
                lblTotal.Text = "Number of Logo(s) : " + LogoCount;
                //LoadDataSourceGrid();
                //this.Page.RegisterClientScriptBlock("RemoveFromFavorite", "<script>alert('This logo has been deleted from your favorite list')</script>");
            }
        }

        protected void imgBtnRefresh_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            LoadDataSourceGrid();
            BindGrid();
        }

        ///<summary>Required method for Designer support</summary>
        private void InitializeComponent() {
        }
        #endregion auto-generated, Initialization code

        private void InitControl() {
            this.DefaultSort = DEFAULT_SORT;
            DVLogo = new DataView(dTblLogo);
            this.DataSource = DVLogo;
            this.MainDataGrid = dtgLogo;
            dtgLogo.DataKeyField = LogoTable.FLD_PKID;
            base.LabelCurrentIndex = lblCurrentIndex;
            base.LabelTotal = lblTotal;
            //clsUtil.SetJScriptForOpenDetail(hypLnkAddNew, QSPForm.Business.AppItem.LogoDetail, BaseLogoDetail.LOGO_ID, "0", 0,0);
        }

        protected override void FillFilter() {
            base.FillFilter();
        }

        public override void BindGrid() {
            this.Page.Validate();
            if (this.Page.IsValid)
                base.BindGrid();
        }

        protected override void LoadDataSourceGrid() {
            QSPForm.Business.LogoSystem logoSys = new LogoSystem();
            dTblLogo = logoSys.SelectAllFavoriteByFMID(this.fm_id);

            DVLogo = new DataView();
            DVLogo.Table = dTblLogo;

            DVLogo.Sort = this.dtgLogo.SortExpression;
            LogoCount = DVLogo.Count;
            lblTotal.Text = "Number of Logo(s) : " + DVLogo.Count.ToString();

            QSPForm.Business.Favorite_LogoSystem fsys = new Favorite_LogoSystem();
            this.DefaultFavoriteList = fsys.SelectAllDefault();
            this.Favorite = fsys.SelectAllLogoIDByFM_ID(this.fm_id);
        }

        protected override void OnItemDataBound(System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                String sID = "0";
                if (e.Item.DataItem != null) {
                    ((ImageButton)e.Item.FindControl("imgBtnDetail")).ImageUrl = QSPForm.Common.QSPFormConfiguration.LogoImagePreviewPath +
                                                                                    ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString() + "." +
                                                                                    QSPForm.Common.QSPFormConfiguration.ImagePreviewFileExtension;

                    sID = ((DataRowView)e.Item.DataItem).Row[dataDef.FLD_PKID].ToString();
                    string sIDName = "logo_id";
                    //clsUtil.SetJScriptForOpenDetail(e.Item, QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0,0);
                    // clsUtil.SetJScriptForOpenDetail(((ImageButton)e.Item.FindControl("imgBtnDetail")), QSPForm.Business.AppItem.LogoDetailInfo, sIDName, sID, 0, 0);
                    clsUtil.SetJScriptForOpenDetailNoCMS(((ImageButton)e.Item.FindControl("imgBtnDetail")), "LogoDetailInfo.aspx?", sIDName, sID, 0, 0);

                    //remove add button if logo is already selected
                    if (this.Favorite.Contains(Convert.ToInt32(sID))) {
                        ((Label)e.Item.FindControl("lblDefaultFavorite")).Visible = false;
                    }
                    else {
                        if (this.DefaultFavoriteList.Contains(Convert.ToInt32(sID))) {
                            ((ImageButton)e.Item.FindControl("imgBtnRemoveFromFavorite")).Visible = false;
                        }
                        else {
                            ((Label)e.Item.FindControl("lblDefaultFavorite")).Visible = false;
                        }
                    }
                }
            }
        }

        private void AddJavascript() {
            txtFMName.Attributes.Add("onfocus", "javascript:window.focus();");
            txtFMID.Attributes.Add("onfocus", "javascript:window.focus();");
            clsUtil.SetJScriptForOpenSelector(imgBtnSelectFM, txtFMID, txtFMName, QSPForm.Business.AppItem.FMSelector, 0, 0);
        }
    }
}