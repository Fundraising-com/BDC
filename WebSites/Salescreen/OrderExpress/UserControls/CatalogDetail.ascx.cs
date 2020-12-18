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
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.CatalogData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    /// Summary description for FormDetail.
    /// </summary>
    public partial class CatalogDetail : BaseWebFormControl {
        private int c_CatalogID = 0;
        public const string CATALOG_ID = "CatalogID";
        private const string CATALOG_DATA = "CatalogData";
        protected dataDef dtsCatalog;

        protected void Page_Load(object sender, System.EventArgs e) {
            try {
                // Put user code to initialize the page here	
                if (!IsPostBack) {
                    if (Request[CATALOG_ID] != null) {
                        c_CatalogID = Convert.ToInt32(Request[CATALOG_ID].ToString());
                    }
                    else {
                        c_CatalogID = 0;
                    }
                    imgBtnDelete.Attributes.Add("onclick", "return confirm('Are you sure that you want to delete this campaign ?');");
                }

                if (ViewState[CATALOG_ID] != null) {
                    c_CatalogID = Int32.Parse(ViewState[CATALOG_ID].ToString());
                }
                ViewState[CATALOG_ID] = c_CatalogID;

                CatalogDetailForm.CatalogID = c_CatalogID;
                CatalogGroupCatalogSubList_Ctrl.CatalogID = c_CatalogID;
                CatalogItemSubList_Ctrl.CatalogID = c_CatalogID;
                CatalogItemCategorySubList_Ctrl.CatalogID = c_CatalogID;

                if (!IsPostBack) {
                    LoadDataSet();

                    //Tab 1 Catalog
                    CatalogDetailForm.DataSource = dtsCatalog.Catalog;
                    CatalogDetailForm.BindForm();
                    //Tab 2 Catalog Group
                    CatalogGroupCatalogSubList_Ctrl.DataSource = dtsCatalog.CatalogGroup;
                    CatalogGroupCatalogSubList_Ctrl.BindForm();
                    //Tab 3 Catalog Item	
                    CatalogItemSubList_Ctrl.DataSource = dtsCatalog.CatalogItem;
                    CatalogItemSubList_Ctrl.BindForm();
                    //					//Tab 4 Category	
                    if (dtsCatalog.Catalog.Rows.Count > 0) {
                        DataRow catRow = dtsCatalog.Catalog.Rows[0];
                        string sCatalogName = catRow[CatalogTable.FLD_NAME].ToString();
                        CatalogItemCategorySubList_Ctrl.CatalogName = sCatalogName;
                    }
                    CatalogItemCategorySubList_Ctrl.DataSource = dtsCatalog.CatalogItemCategory;
                    CatalogItemCategorySubList_Ctrl.BindForm();
                }
                else {
                    //For each postback, the page (the higher in the hierarchy)
                    //is in charge to set all children's datasource 
                    dtsCatalog = (CatalogData)this.ViewState[CATALOG_DATA];
                    CatalogDetailForm.DataSource = dtsCatalog.Catalog;
                    CatalogGroupCatalogSubList_Ctrl.DataSource = dtsCatalog.CatalogGroup;
                    CatalogItemSubList_Ctrl.DataSource = dtsCatalog.CatalogItem;
                    if (dtsCatalog.Catalog.Rows.Count > 0) {
                        DataRow catRow = dtsCatalog.Catalog.Rows[0];
                        string sCatalogName = catRow[CatalogTable.FLD_NAME].ToString();
                        CatalogItemCategorySubList_Ctrl.CatalogName = sCatalogName;
                    }
                    CatalogItemCategorySubList_Ctrl.DataSource = dtsCatalog.CatalogItemCategory;
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnDelete.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnDelete_Click);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
        }
        #endregion
        
        public int FormID {
            get {
                return c_CatalogID;
            }
            set {
                c_CatalogID = value;
                ViewState[CATALOG_ID] = c_CatalogID;
            }
        }

        private void SetHeaderText() {
            if (dtsCatalog.Catalog.Rows.Count > 0) {
                DataRow catRow = dtsCatalog.Catalog.Rows[0];
                lblCatalogTitle.Text = "Catalog :  " + catRow[CatalogTable.FLD_CODE].ToString() + " - " + catRow[CatalogTable.FLD_NAME].ToString();
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            SetHeaderText();
            this.ViewState[CATALOG_DATA] = dtsCatalog;
        }

        private void LoadDataSet() {
            if (this.c_CatalogID > 0) {
                QSPForm.Business.CatalogSystem objSys = new QSPForm.Business.CatalogSystem();
                dtsCatalog = objSys.SelectAllDetail(c_CatalogID);
            }
            else {
                //Insert a new row
                dtsCatalog = new CatalogData();
                DataRow newRow = dtsCatalog.Catalog.NewRow();
                newRow[CatalogTable.FLD_NAME] = "New Catalog";
                dtsCatalog.Catalog.Rows.Add(newRow);
            }
        }

        private void imgBtnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            try {
                Boolean blnValid = true;
                blnValid = CatalogDetailForm.IsValid();
                if (!blnValid) {
                    TbStrp_Form.SelectedIndex = 0;
                    return;
                }
                //				blnValid = CatalogGroupCatalogSubList_Ctrl.IsValid();
                //				if (!blnValid)
                //				{
                //					TbStrp_Form.SelectedIndex = 1;
                //					return;
                //				}	
                //				blnValid = CatalogItemSubList_Ctrl.IsValid();
                //				if (!blnValid)
                //				{
                //					TbStrp_Form.SelectedIndex = 2;
                //					return;
                //				}	
                //				blnValid = CatalogItemCategorySubList_Ctrl.IsValid();
                //				if (!blnValid)
                //				{
                //					TbStrp_Form.SelectedIndex = 3;
                //					return;
                //				}	

                blnValid = CatalogDetailForm.UpdateDataSource();
                if (!blnValid) {
                    return;
                }
                //				blnValid = CatalogGroupCatalogSubList_Ctrl.UpdateDataSource();
                //				if (!blnValid)
                //				{						
                //					return;
                //				}
                //				blnValid = CatalogItemSubList_Ctrl.UpdateDataSource();
                //				if (!blnValid)
                //				{						
                //					return;
                //				}
                //				blnValid = CatalogItemCategorySubList_Ctrl.UpdateDataSource();
                //				if (!blnValid)
                //				{						
                //					return;
                //				}

                QSPForm.Business.CatalogSystem CatalogSystem = new QSPForm.Business.CatalogSystem();
                blnValid = CatalogSystem.UpdateAllDetail(dtsCatalog);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }
    }
}