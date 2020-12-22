using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.FormCatalogGroupTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FormCatalogGroupList.
    /// </summary>
    public partial class FormCatalogGroupForm : BaseWebUserControl {
        protected dataDef dTblFormCatalogGroup = new dataDef();
        private int c_ParentID = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected CatalogGroupTable tblCatalogGroup = new CatalogGroupTable();
        protected CatalogItemCategoryTable tblCatalogItemCategory = new CatalogItemCategoryTable();
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        bool c_IsReadOnly = false;
        bool c_HideButton = false;
        protected DataView DVFormCatalogGroup;
        protected DataView DVCatalogGroup;
        protected DataView DVCatalogItemCategory;
        protected int c_EntityTypeID = 0;
        string sPreviousCatalogGroupID = "";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here	
            AssignEventToDropDown();
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
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
            this.dtLstFormCatalogGroup.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstFormCatalogGroup_ItemCreated);
            this.dtLstFormCatalogGroup.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtLstFormCatalogGroup_DeleteCommand);
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

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                imgBtnAddNew.Visible = !c_HideButton;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dTblFormCatalogGroup = bizSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            sPreviousCatalogGroupID = "";
            FillDataTableForDropDownList();
            DVFormCatalogGroup = new DataView(dTblFormCatalogGroup);
            dtLstFormCatalogGroup.DataBind();
        }

        public int Count {
            get {
                return this.dtLstFormCatalogGroup.Items.Count;
            }
        }

        public FormCatalogGroupTable DataSource {
            get {
                return dTblFormCatalogGroup;

            }
            set {
                dTblFormCatalogGroup = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstFormCatalogGroup.RepeatDirection;
            }
            set {
                dtLstFormCatalogGroup.RepeatDirection = value;
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

        public int EntityTypeID {
            get {
                return c_EntityTypeID;
            }
            set {
                c_EntityTypeID = value;
            }
        }

        public bool IsReadOnly {
            get {
                return c_IsReadOnly;
            }
            set {
                c_IsReadOnly = value;
            }
        }

        public bool HideButton {
            get {
                return c_HideButton;
            }
            set {
                c_HideButton = value;
            }
        }

        protected int getSelectedIndex(DataView dv, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCount = 0;
                    foreach (DataRowView dvRow in dv) {
                        if (sValue == dvRow[0].ToString()) {
                            iIndex = iCount;
                            break;
                        }
                        iCount++;
                    }
                }

            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return iIndex;

        }

        protected DataView getCatalogCategoryDataView(String sCatalogGroupID) {
            try {
                if (sCatalogGroupID == "")
                    sCatalogGroupID = "0";
                if (sCatalogGroupID != sPreviousCatalogGroupID) {
                    //Catalog Item Category
                    if (sCatalogGroupID == "0") {	//Don't need to query thd DB for that.
                        tblCatalogItemCategory = new CatalogItemCategoryTable();
                        tblCatalogItemCategory.Rows.InsertAt(tblCatalogItemCategory.NewRow(), 0);
                        DVCatalogItemCategory = new DataView(tblCatalogItemCategory);
                    }
                    else {
                        QSPForm.Business.CatalogItemCategorySystem catItemCategSys = new QSPForm.Business.CatalogItemCategorySystem();
                        tblCatalogItemCategory = catItemCategSys.SelectAllByCatalogGroupID(Convert.ToInt32(sCatalogGroupID));
                        tblCatalogItemCategory.Rows.InsertAt(tblCatalogItemCategory.NewRow(), 0);
                        DVCatalogItemCategory = new DataView(tblCatalogItemCategory);
                    }
                }

                sPreviousCatalogGroupID = sCatalogGroupID;
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return DVCatalogItemCategory;
        }

        private void FillDataTableForDropDownList() {
            try {
                //Catalog Group	
                QSPForm.Business.CatalogGroupSystem catGrpSys = new QSPForm.Business.CatalogGroupSystem();
                tblCatalogGroup = catGrpSys.SelectAll();
                tblCatalogGroup.Rows.InsertAt(tblCatalogGroup.NewRow(), 0);
                DVCatalogGroup = new DataView(tblCatalogGroup);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                int iCounter = 0;
                CommonUtility clsUtil = new CommonUtility();
                //'We save everything that is possible				
                for (iCounter = 0; iCounter <= dtLstFormCatalogGroup.Items.Count - 1; iCounter++) {
                    DataListItem dlstItem;
                    dlstItem = dtLstFormCatalogGroup.Items[iCounter];
                    DataView dv = new DataView(dTblFormCatalogGroup);
                    int ID = Convert.ToInt32(dtLstFormCatalogGroup.DataKeys[iCounter]);
                    dv.Sort = dataDef.FLD_PKID;
                    int iIndex = dv.Find(ID);
                    if (iIndex != -1) {
                        DataRow row = dv[iIndex].Row;
                        if (row.RowState != DataRowState.Deleted) {
                            //'Table Mapping
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_ID, c_ParentID.ToString());
                            DropDownList ddl;
                            string sValue;
                            //Catalog Group
                            ddl = ((DropDownList)dlstItem.FindControl("ddlCatalogGroup"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_GROUP_ID, sValue);
                            //Product Catalog category
                            ddl = ((DropDownList)dlstItem.FindControl("ddlProductCategory"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_PRODUCT_CATALOG_ITEM_CATEGORY_ID, sValue);
                            //Supply Catalog category
                            ddl = ((DropDownList)dlstItem.FindControl("ddlSupplyCategory"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_SUPPLY_CATALOG_ITEM_CATEGORY_ID, sValue);

                            if (row.RowState == DataRowState.Added)
                                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                            else
                                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                        }
                    }
                }

                blnValid = true;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            AddNew();
        }

        private void dtLstFormCatalogGroup_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e) {
            Delete(e.Item.ItemIndex);
        }

        public void Delete(int iItemIndex) {
            UpdateDataSource();
            DataView dv = new DataView(dTblFormCatalogGroup);
            int ID = Convert.ToInt32(dtLstFormCatalogGroup.DataKeys[iItemIndex]);
            dv.Sort = dataDef.FLD_PKID;
            int iIndex = dv.Find(ID);
            if (iIndex != -1) {
                DataRow row = dv[iIndex].Row;
                if (row.RowState != DataRowState.Deleted) {
                    if (row.RowState != DataRowState.Added)
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;

                    row.Delete();
                }
            }
            BindForm();
        }

        public void AddNew() {
            UpdateDataSource();
            dTblFormCatalogGroup.Rows.Add(dTblFormCatalogGroup.NewRow());
            BindForm();
        }

        private void dtLstFormCatalogGroup_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null) {
                    imgBtnDelete.Visible = (!c_HideButton);
                }
            }
        }

        private void AssignEventToDropDown() {
            int iCounter;
            for (iCounter = 0; iCounter <= dtLstFormCatalogGroup.Items.Count - 1; iCounter++) {
                DataListItem dlstItem;
                dlstItem = dtLstFormCatalogGroup.Items[iCounter];
                DropDownList ddl = ((DropDownList)dlstItem.FindControl("ddlCatalogGroup"));
                if (ddl != null)
                    ddl.SelectedIndexChanged += new EventHandler(ddlCatalogGroup_SelectedIndexChanged);
            }
        }

        private void ddlCatalogGroup_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateDataSource();
            BindForm();
        }
    }
}