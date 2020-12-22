using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.FormProfitRateTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FormProfitRateList.
    /// </summary>
    public partial class FormProfitRateList : BaseWebUserControl {
        protected dataDef dTblFormProfitRate = new dataDef();
        private int c_ParentID = 0;
        protected DataTable dTblProfitRate = new DataTable();
        protected DataView DVFormProfitRate;
        protected int c_EntityTypeID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here				
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
            this.dtgFormProfitRate.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgFormProfitRate_ItemCreated);
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
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dTblFormProfitRate = bizSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            DVFormProfitRate = new DataView(dTblFormProfitRate);
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            dTblProfitRate = comSys.SelectAllProfitRate();
            dtgFormProfitRate.DataBind();
        }

        public int Count {
            get {
                return this.dtgFormProfitRate.Items.Count;
            }
        }

        public FormProfitRateTable DataSource {
            get {
                return dTblFormProfitRate;

            }
            set {
                dTblFormProfitRate = value;
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

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                int iCounter = 0;
                CommonUtility clsUtil = new CommonUtility();
                //'We save everything that is possible				
                for (iCounter = 0; iCounter <= dtgFormProfitRate.Items.Count - 1; iCounter++) {
                    DataGridItem dtgItem;
                    dtgItem = dtgFormProfitRate.Items[iCounter];
                    //Catalog Group
                    CheckBox chk = ((CheckBox)dtgItem.FindControl("chkSelected"));
                    bool IsSelected = chk.Checked;

                    DataView dv = new DataView(dTblFormProfitRate);
                    int ID = Convert.ToInt32(dtgFormProfitRate.DataKeys[iCounter]);
                    dv.Sort = dataDef.FLD_PROFIT_RATE_ID;
                    int iIndex = dv.Find(ID);

                    if (iIndex == -1) {
                        if (IsSelected) {
                            //Add New
                            DataRow newRow = dTblFormProfitRate.NewRow();
                            newRow[dataDef.FLD_FORM_ID] = c_ParentID;
                            newRow[dataDef.FLD_PROFIT_RATE_ID] = ID;
                            //newRow[dataDef.FLD_PROFIT_RATE] = chk.Text;
                            newRow[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                            dTblFormProfitRate.Rows.Add(newRow);
                        }
                    }
                    else //Already exist
                    {
                        DataRow row = dv[iIndex].Row;

                        if (!IsSelected) {
                            if (row.RowState != DataRowState.Deleted) {
                                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                                row.Delete();
                            }
                            else {
                                row.RejectChanges();
                            }
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

        private void dtgFormProfitRate_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                CheckBox chkSelected = ((CheckBox)e.Item.FindControl("chkSelected"));
                if (chkSelected != null) {
                    DataView dv = new DataView(dTblFormProfitRate);
                    int ID = Convert.ToInt32(dtgFormProfitRate.DataKeys[e.Item.ItemIndex]);
                    dv.Sort = dataDef.FLD_PROFIT_RATE_ID;
                    int iIndex = dv.Find(ID);
                    chkSelected.Checked = (iIndex > -1);
                }
            }
        }
    }
}