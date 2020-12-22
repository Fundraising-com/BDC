using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.BusinessFieldTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class BusinessFieldList : BaseWebUserControl {
        protected dataDef dtBusinessField = new dataDef();
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected System.Data.DataTable tblTypeBusinessField = new DataTable();
        private QSPForm.Business.BusinessFieldSystem bizfldSys = new QSPForm.Business.BusinessFieldSystem();
        private const string DATASOURCE = "Data_Source";

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					
            if (!IsPostBack) {
                dtBusinessField = new dataDef();
                LoadDataSource();
                this.DataBind();
            }
            else {
                dtBusinessField = (dataDef)this.ViewState[DATASOURCE];
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
            this.dtgBusinessField.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgBusinessField_DeleteCommand);
            this.imgBtnSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSave_Click);
            this.PreRender += new System.EventHandler(this.BusinessFieldList_PreRender);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList

                this.dtgBusinessField.EditItemIndex = -1;
                BindGrid();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public BusinessFieldTable DataSource {
            get {
                return dtBusinessField;

            }
            set {
                dtBusinessField = value;
            }
        }

        public void LoadDataSource() {

            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.
            dtBusinessField = bizfldSys.SelectAll();
            if (dtBusinessField.Rows.Count == 0) {
                imgBtnAddNew_Click(null, null);
            }
        }

        private void BindGrid() {
            FillDataTableForDropDownList();
            this.dtgBusinessField.DataBind();
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            UpdateDataSource();
            DataRow NewRow = dtBusinessField.NewRow();
            dtBusinessField.Rows.InsertAt(NewRow, 0);
            BindGrid();
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Type Field				
                tblTypeBusinessField = comSys.SelectAllBusinessFieldType();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCount = 0;
                    foreach (DataRow row in dt.Rows) {
                        if (sValue == row[0].ToString()) {
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

        private void dtgBusinessField_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            UpdateDataSource();
            DataView dv = new DataView(dtBusinessField);
            int ID = Convert.ToInt32(dtgBusinessField.DataKeys[e.Item.ItemIndex]);
            dv.Sort = BusinessFieldTable.FLD_PKID;
            int iIndex = dv.Find(ID);
            if (iIndex != -1) {
                DataRow row = dv[iIndex].Row;
                if (row.RowState != DataRowState.Deleted) {
                    if (row.RowState != DataRowState.Added)
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;

                    row.Delete();
                }
            }
            BindGrid();
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                CommonUtility clsUtil = new CommonUtility();
                int iCounter = 0;
                //'We save everything that is possible
                //Invalid 	
                for (iCounter = 0; iCounter <= dtgBusinessField.Items.Count - 1; iCounter++) {
                    DataGridItem dgItem;
                    dgItem = dtgBusinessField.Items[iCounter];
                    DataView dv = new DataView(dtBusinessField);
                    int ID = Convert.ToInt32(dtgBusinessField.DataKeys[iCounter]);
                    dv.Sort = BusinessFieldTable.FLD_PKID;
                    int iIndex = dv.Find(ID);
                    if (iIndex != -1) {
                        DataRow row = dv[iIndex].Row;
                        if (row.RowState != DataRowState.Deleted) {
                            //'Table Mapping                      
                            clsUtil.UpdateRow(row, dataDef.FLD_FIELD_TYPE_ID, ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue);
                            clsUtil.UpdateRow(row, dataDef.FLD_FIELD_TYPE_NAME, ((DropDownList)dgItem.FindControl("ddlType")).SelectedItem.Text);
                            clsUtil.UpdateRow(row, dataDef.FLD_FIELD_NAME, ((TextBox)dgItem.FindControl("txtName")).Text);
                            clsUtil.UpdateRow(row, dataDef.FLD_DESCRIPTION, ((TextBox)dgItem.FindControl("txtDescription")).Text);
                            clsUtil.UpdateRow(row, dataDef.FLD_IS_FORM_PROPERTY, ((CheckBox)dgItem.FindControl("chkBoxIsFormProperty")).Checked.ToString());
                            clsUtil.UpdateRow(row, dataDef.FLD_IS_APPLY_TO_ACCOUNT, ((CheckBox)dgItem.FindControl("chkBoxApplyToAccount")).Checked.ToString());
                            clsUtil.UpdateRow(row, dataDef.FLD_IS_APPLY_TO_CREDIT_APPLICATION, ((CheckBox)dgItem.FindControl("chkBoxApplyToCreditApp")).Checked.ToString());
                            clsUtil.UpdateRow(row, dataDef.FLD_IS_APPLY_TO_ORDER, ((CheckBox)dgItem.FindControl("chkBoxApplyToOrder")).Checked.ToString());
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

        protected void BusinessFieldList_PreRender(object sender, EventArgs e) {
            this.ViewState[DATASOURCE] = dtBusinessField;
        }

        private void imgBtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            bool blnSuccess = false;
            try {
                blnSuccess = UpdateDataSource();
                if (blnSuccess) {
                    blnSuccess = bizfldSys.UpdateBatch(dtBusinessField);

                }
                //LoadDataSource();
                BindGrid();
            }
            catch (Exception ex) {
                if (ex is QSPForm.Common.QSPFormValidationException) {
                    this.Page.MaintainScrollPositionOnPostBack = false;
                    CommonUtility clsUtil = new CommonUtility();
                    clsUtil.RenderStartUpScroll(this.Page.ValSummary);
                }
                Page.SetPageError(ex);
            }
        }
    }
}