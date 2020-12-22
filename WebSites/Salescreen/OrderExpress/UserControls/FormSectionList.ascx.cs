using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.FormSectionTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FormSectionList.
    /// </summary>
    public partial class FormSectionList : BaseWebUserControl {
        protected dataDef dTblFormSection = new dataDef();
        private int c_ParentID = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        protected DataTable tblFormSectionType = new DataTable();
        protected CatalogTable tblCatalog = new CatalogTable();
        protected CatalogItemCategoryTable tblCatalogItemCategory = new CatalogItemCategoryTable();
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        bool c_IsReadOnly = false;
        bool c_HideButton = false;
        protected DataView DVFormSection;
        protected DataView DVFormSectionType;
        protected DataView DVCatalog;
        private DataView DVCatalogItemCategory;
        protected int c_EntityTypeID = 0;
        string sPreviousCatalogID = "";

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
            this.dtLstFormSection.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstFormSection_ItemCreated);
            this.dtLstFormSection.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtLstFormSection_DeleteCommand);
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
            //dTblFormSection = bizSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            sPreviousCatalogID = "";
            FillDataTableForDropDownList();
            DVFormSection = new DataView(dTblFormSection);
            dtLstFormSection.DataBind();
        }

        public int Count {
            get {
                return this.dtLstFormSection.Items.Count;
            }
        }

        public FormSectionTable DataSource {
            get {
                return dTblFormSection;
            }
            set {
                dTblFormSection = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstFormSection.RepeatDirection;
            }
            set {
                dtLstFormSection.RepeatDirection = value;
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

        protected DataView getCatalogCategoryDataView(String sCatalogID) {
            try {
                if (sCatalogID == "")
                    sCatalogID = "0";
                if (sCatalogID != sPreviousCatalogID) {
                    //Catalog Item Category
                    if (sCatalogID == "0") {	//Don't need to query thd DB for that.
                        tblCatalogItemCategory = new CatalogItemCategoryTable();
                        tblCatalogItemCategory.Rows.InsertAt(tblCatalogItemCategory.NewRow(), 0);
                        DVCatalogItemCategory = new DataView(tblCatalogItemCategory);
                    }
                    else {
                        QSPForm.Business.CatalogItemCategorySystem catItemCategSys = new QSPForm.Business.CatalogItemCategorySystem();
                        tblCatalogItemCategory = catItemCategSys.SelectAllByCatalogID(Convert.ToInt32(sCatalogID));
                        tblCatalogItemCategory.Rows.InsertAt(tblCatalogItemCategory.NewRow(), 0);
                        DVCatalogItemCategory = new DataView(tblCatalogItemCategory);
                    }
                }

                sPreviousCatalogID = sCatalogID;
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return DVCatalogItemCategory;
        }

        protected int getCatalogItemCategorySelectedIndex(String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCount = 0;
                    foreach (DataRowView dvRow in DVCatalogItemCategory) {
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

        private void FillDataTableForDropDownList() {
            try {
                //Catalog Group	
                QSPForm.Business.CatalogSystem catSys = new QSPForm.Business.CatalogSystem();
                tblCatalog = catSys.SelectAllByCatalogGroupID(1);
                tblCatalog.Rows.InsertAt(tblCatalog.NewRow(), 0);
                DVCatalog = new DataView(tblCatalog);

                //Section Type
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                tblFormSectionType = comSys.SelectAllFormSectionType();
                DataRow newRow = tblFormSectionType.NewRow();
                newRow[0] = 0;
                newRow[1] = "---SELECT---";
                tblFormSectionType.Rows.InsertAt(newRow, 0);
                DVFormSectionType = new DataView(tblFormSectionType);
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
                for (iCounter = 0; iCounter <= dtLstFormSection.Items.Count - 1; iCounter++) {
                    DataListItem dlstItem;
                    dlstItem = dtLstFormSection.Items[iCounter];
                    DataView dv = new DataView(dTblFormSection);
                    int ID = Convert.ToInt32(dtLstFormSection.DataKeys[iCounter]);
                    dv.Sort = dataDef.FLD_PKID;
                    int iIndex = dv.Find(ID);
                    if (iIndex != -1) {
                        DataRow row = dv[iIndex].Row;
                        if (row.RowState != DataRowState.Deleted) {
                            //'Table Mapping
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_ID, c_ParentID.ToString());
                            DropDownList ddl;
                            string sValue;
                            //Catalog
                            ddl = ((DropDownList)dlstItem.FindControl("ddlCatalog"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ID, sValue);

                            //Catalog category
                            ddl = ((DropDownList)dlstItem.FindControl("ddlCatalogCategory"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_CATALOG_ITEM_CATEGORY_ID, sValue);
                            //Form Section Type
                            ddl = ((DropDownList)dlstItem.FindControl("ddlFormSectionType"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_TYPE_ID, sValue);

                            //Form Section Number
                            TextBox txt;
                            txt = ((TextBox)dlstItem.FindControl("txtSectionNumber"));
                            sValue = txt.Text;
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_NUMBER, sValue);
                            //Form Section Title
                            txt = ((TextBox)dlstItem.FindControl("txtSectionTitle"));
                            sValue = txt.Text;
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_TITLE, sValue);
                            //Description
                            txt = ((TextBox)dlstItem.FindControl("txtDescription"));
                            sValue = txt.Text;
                            clsUtil.UpdateRow(row, dataDef.FLD_DESCRIPTION, sValue);


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

        private void dtLstFormSection_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e) {
            Delete(e.Item.ItemIndex);
        }

        public void Delete(int iItemIndex) {
            UpdateDataSource();
            DataView dv = new DataView(dTblFormSection);
            int ID = Convert.ToInt32(dtLstFormSection.DataKeys[iItemIndex]);
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
            DataRow newRow = dTblFormSection.NewRow();
            newRow[dataDef.FLD_FORM_ID] = c_ParentID;
            dTblFormSection.Rows.Add(newRow);
            BindForm();
        }

        private void dtLstFormSection_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null) {
                    imgBtnDelete.Visible = (!c_HideButton);
                }
            }
        }

        private void AssignEventToDropDown() {
            //int iCounter;
            //for (iCounter = 0;iCounter <= dtLstFormSection.Items.Count - 1;iCounter++)
            //{
            //    DataListItem dlstItem;
            //    dlstItem = dtLstFormSection.Items[iCounter];
            //    DropDownList ddl = ((DropDownList) dlstItem.FindControl("ddlCatalog"));
            //    if (ddl != null)
            //        ddl.SelectedIndexChanged +=new EventHandler(ddlCatalog_SelectedIndexChanged);
            //}
        }

        protected void ddlCatalog_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateDataSource();
            BindForm();
        }
    }
}